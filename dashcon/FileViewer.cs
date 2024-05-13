using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using NsExcel = Microsoft.Office.Interop.Excel;

namespace dashcon
{
    public partial class FileViewer : UserControl
    {
        public const int DUNITS = 4;
        const byte CHUNK_TERM = 0x00;
        const byte CHUNK_SYMBOL = 0x01;
        const byte CHUNK_LINE = 0x02;
        const byte CHUNK_TEXT = 0x03;

        const byte LINE_GLYPH_START_BLOB = 0x01;
        const byte LINE_GLYPH_END_BLOB = 0x02;
        const byte LINE_GLYPH_START_ARROW = 0x04;
        const byte LINE_GLYPH_END_ARROW = 0x08;

        const byte TEXT_STYLE_OVERSCORE = 0x01;
        const byte TEXT_STYLE_HIDE = 0x04;         // not sure about this - PS terms on gates (hide)
        const byte TEXT_FONT_SIZE = 0xE0;
        const byte TEXT_FONT_VERTICAL = 0x04;

        const byte LINE_TYPE_DASHED = 0x04;

        public FileHeader header = new FileHeader();
        public List<SymbolElement> symbols = new List<SymbolElement>();
        public List<LineElement> lines = new List<LineElement>();
        public List<TextElement> text = new List<TextElement>();
        public List<byte[]> elementCollection = new List<byte[]>();

        float zoom = 1.0F;
        string symbolCode;
        string searchText;
        Point linePoint;
        bool showSymbolID = false;

        string reportStr;


        public class FileHeader
        {
            public byte[] h1 { get; set; }       // 7 bytes
            public short width { get; set; }
            public short height { get; set; }
            public short h2 { get; set; }
            public byte size { get; set; }
            public short h3 { get; set; }
            public byte[] h4 { get; set; }      // 64 bytes
        };

        public class SymbolElement
        {
            public byte chunk { get; set; }
            public byte size { get; set; }
            public byte layer { get; set; }
            public short x { get; set; }
            public short y { get; set; }
            public short width { get; set; }
            public short height { get; set; }
            public short id { get; set; }
            public short length { get; set; }
            public byte transform { get; set; }
            public sbyte[] bytes2 { get; set; }

            public SymbolElement(byte _size)
            {
                chunk = CHUNK_SYMBOL;
                size = _size;

                this.bytes2 = new sbyte[(size - 1) * 16];
            }
        }

        public class LineElement
        {
            public byte chunk { get; set; }
            public byte size { get; set; }
            public byte glyph { get; set; }
            public short x { get; set; }
            public short y { get; set; }
            public short width { get; set; }
            public short height { get; set; }
            public short type { get; set; }
            public byte[] attr { get; set; }
            public byte pad { get; set; }

            public LineElement(byte _size)
            {
                chunk = CHUNK_LINE;
                size = _size;

            }
        }

        public class TextElement
        {
            public byte chunk { get; set; }
            public byte size { get; set; }
            public byte layer { get; set; }
            public short x { get; set; }
            public short y { get; set; }
            public short width { get; set; }
            public short height { get; set; }
            public byte font { get; set; }
            public byte style { get; set; }
            public byte[] flags { get; set; }
            public byte pad { get; set; }

            public string text { get; set; }

            public TextElement(byte _size)
            {
                chunk = CHUNK_TEXT;
                size = _size;
            }
        }

        public List<byte[]> ElementCollection
        {
            get
            {
                return (elementCollection);
            }
            set
            {
                elementCollection = value;
            }
        }

        private string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                LoadFile();
            }
        }

        public int lineCount;


        public FileViewer()
        {
            InitializeComponent();
            loadElements();
        }

        public void LoadFile()
        {
            reportStr = "DASHCON Report for " + fileName + "\r\n";
            reportStr += "Copyright (C) 2016 Contrelec Limited. All rights reserved.\r\n";
            reportStr += "==========================================================\r\n";
            symbols.Clear();
            lines.Clear();
            text.Clear();
            process();
            Size limits = GetDWGExtents();
            limits.Width += 1;
            limits.Height += 1;
            pictureBox1.Size = new Size(limits.Width * 4, limits.Height * 4);
            Graphics dwgSheet = pictureBox1.CreateGraphics();
            dwgSheet.Clear(Color.WhiteSmoke);
            pictureBox1_Paint(this, new PaintEventArgs(dwgSheet, new Rectangle(Convert.ToInt32(dwgSheet.ClipBounds.Left),
                                                                               Convert.ToInt32(dwgSheet.ClipBounds.Top),
                                                                               Convert.ToInt32(dwgSheet.ClipBounds.Width),
                                                                               Convert.ToInt32(dwgSheet.ClipBounds.Height))));

            reportStr += "Processed " + lines.Count + " lines, " + text.Count + " text items and " + symbols.Count + " symbols.\r\n";
        }

        public void viewReport()
        {
            ReportViewer f = new ReportViewer();
            f.Text = "Conversion Report for " + fileName;
            f.ReportText = reportStr;
            f.Show();
        }


        private void process()
        {
            if ((fileName == null) || (fileName == ""))
            {
                return;
            }

            if (!File.Exists(fileName))
            {
                MessageBox.Show("File does not exist (" + fileName + ")", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    header.h1 = reader.ReadBytes(7);
                    header.width = reader.ReadInt16();
                    header.height = reader.ReadInt16();
                    header.h2 = reader.ReadInt16();
                    header.size = reader.ReadByte();
                    header.h3 = reader.ReadInt16();
                    header.h1 = reader.ReadBytes(64);

                    bool eof = false;
                    while ((reader.BaseStream.Position != reader.BaseStream.Length) && !eof)
                    {

                        byte type = reader.ReadByte();
                        byte size = reader.ReadByte();
                        switch (type)
                        {
                            case CHUNK_TERM:
                                //reader.ReadBytes(15);
                                eof = true;
                                break;
                            case CHUNK_SYMBOL:
                                SymbolElement el1 = new SymbolElement(size);
                                el1.layer = reader.ReadByte();
                                el1.x = reader.ReadInt16();
                                el1.y = reader.ReadInt16();
                                el1.width = reader.ReadInt16();
                                el1.height = reader.ReadInt16();
                                el1.id = reader.ReadInt16();
                                el1.length = reader.ReadInt16();
                                el1.transform = reader.ReadByte();
                                for (int i = 0; i < (size - 1) * 16; i++)
                                {
                                    el1.bytes2[i] = reader.ReadSByte();
                                }
                                symbols.Add(el1);
                                break;
                            case CHUNK_TEXT:
                                TextElement el2 = new TextElement(size);
                                el2.layer = reader.ReadByte();
                                el2.x = reader.ReadInt16();
                                el2.y = reader.ReadInt16();
                                el2.width = reader.ReadInt16();
                                el2.height = reader.ReadInt16();
                                el2.font = reader.ReadByte();
                                el2.style = reader.ReadByte();
                                el2.flags = reader.ReadBytes(2);
                                el2.pad = reader.ReadByte();
                                Byte[] bytes = reader.ReadBytes((size - 1) * 16);
                                string t = Encoding.ASCII.GetString(bytes);
                                string[] ta = t.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);

                                el2.text = ta[0];
                                text.Add(el2);
                                break;
                            case CHUNK_LINE:
                                LineElement el3 = new LineElement(size);
                                el3.glyph = reader.ReadByte();
                                el3.x = reader.ReadInt16();
                                el3.y = reader.ReadInt16();
                                el3.width = reader.ReadInt16();
                                el3.height = reader.ReadInt16();
                                el3.type = reader.ReadByte();
                                el3.attr = reader.ReadBytes(4);
                                lines.Add(el3);
                                break;
                            default:
                                Debug.WriteLine("Unrecognised chunk: " + type.ToString());
                                break;

                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception e)
            {
                this.Cursor = Cursors.Default;
                reportStr += e.Message + "\r\n";
                MessageBox.Show("Error reading file " + e.Message, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }


        private void dLines(Graphics dwgSheet)
        {
            Point p1;
            Point p2;
            Point p3;

            float arrowHeight = 1.5F;
            float arrowWidth = 3.0F;

            arrowHeight = Convert.ToInt32(arrowHeight * DUNITS * zoom);
            arrowWidth = Convert.ToInt32(arrowWidth * DUNITS * zoom);

            int blobW = 1;
            int blobH = 1;

            blobW = Convert.ToInt32(blobW * DUNITS * zoom);
            blobH = Convert.ToInt32(blobH * DUNITS * zoom);

            foreach (LineElement l in lines)
            {
                int thickness = 1;
                float[] dashValues = null;

                switch (l.type)
                {
                    case 0:
                        thickness = 1;
                        break;
                    case 1:
                        thickness = 4;
                        break;
                    case 2:
                        thickness = 1;
                        dashValues = new float[] { 2F * zoom, 2F * zoom };
                        break;
                    case 3:
                        thickness = 1;
                        dashValues = new float[] { 1F * zoom, 3F * zoom };
                        break;
                    case 4:
                        thickness = 1;
                        dashValues = new float[] { 11F * zoom, 5F * zoom };
                        break;
                    case 5:
                        thickness = 1;
                        dashValues = new float[] { 1F * zoom, 1F * zoom };
                        break;
                    case 6:
                        thickness = 2;
                        break;
                    default:
                        thickness = 1;
                        dashValues = new float[] { 1F * zoom, 8F * zoom };
                        break;
                }

                Pen thisPen = new Pen(Color.Black, thickness * zoom);

                if (linePoint == new Point(l.x, l.y))
                {
                    thisPen = new Pen(Color.Lime, thickness * zoom);
                }
                if (dashValues != null) thisPen.DashPattern = dashValues;
                Rectangle r = new Rectangle(Convert.ToInt32(DUNITS * zoom * l.x),
                                            Convert.ToInt32(DUNITS * zoom * l.y),
                                            Convert.ToInt32(DUNITS * zoom * l.width),
                                            Convert.ToInt32(DUNITS * zoom * l.height));
                /*if (l.width == 0 || l.height == 0)
                { */
                    dwgSheet.DrawLine(thisPen,
                            new Point(Convert.ToInt32(DUNITS * zoom * l.x), Convert.ToInt32(DUNITS * zoom * l.y)),
                            new Point(Convert.ToInt32(DUNITS * zoom * (l.x + l.width)), Convert.ToInt32(DUNITS * zoom * (l.y + l.height))));
                /*
                }
                else
                {
                    dwgSheet.DrawRectangle(new Pen(Color.Red, 1 * zoom), r);
                }   */

                if ((l.glyph & LINE_GLYPH_START_BLOB) > 0)
                {
                    Rectangle rb = new Rectangle(Convert.ToInt32((DUNITS * zoom * l.x) - (blobW / 2)),
                                                 Convert.ToInt32((DUNITS * zoom * l.y) - (blobH / 2)),
                                                    blobW, blobH);
                    dwgSheet.DrawEllipse(thisPen, rb);
                }

                if ((l.glyph & LINE_GLYPH_END_BLOB) > 0)
                {
                    Rectangle rb = new Rectangle(Convert.ToInt32((DUNITS * zoom * (l.x + l.width)) - (blobW / 2)),
                                                 Convert.ToInt32((DUNITS * zoom * (l.y + l.height)) - (blobH / 2)),
                                blobW, blobH);
                    dwgSheet.DrawEllipse(thisPen, rb);
                }

                if ((l.glyph & LINE_GLYPH_START_ARROW) > 0)
                {
                    if (l.height == 0)
                    {
                        p1 = new Point(Convert.ToInt32(DUNITS * zoom * l.x), Convert.ToInt32(DUNITS * zoom * l.y));
                        p2 = new Point(Convert.ToInt32((DUNITS * zoom * (l.x)) + arrowWidth), Convert.ToInt32((DUNITS * zoom * (l.y)) + (arrowHeight / 2)));
                        p3 = new Point(Convert.ToInt32((DUNITS * zoom * (l.x)) + arrowWidth), Convert.ToInt32((DUNITS * zoom * (l.y)) - (arrowHeight / 2)));
                    }
                    else
                    {
                        p1 = new Point(Convert.ToInt32(DUNITS * zoom * l.x), Convert.ToInt32(DUNITS * zoom * l.y));
                        p2 = new Point(Convert.ToInt32((DUNITS * zoom * (l.x)) - (arrowHeight / 2)), Convert.ToInt32((DUNITS * zoom * (l.y)) + (arrowWidth)));
                        p3 = new Point(Convert.ToInt32((DUNITS * zoom * (l.x)) + (arrowHeight / 2)), Convert.ToInt32((DUNITS * zoom * (l.y)) + (arrowWidth)));

                    }
                    dwgSheet.DrawLine(thisPen, p1, p2);
                    dwgSheet.DrawLine(thisPen, p1, p3);
                    dwgSheet.DrawLine(thisPen, p2, p3);
                }

                if ((l.glyph & LINE_GLYPH_END_ARROW) > 0)
                {
                    if (l.height == 0)
                    {
                        p1 = new Point(Convert.ToInt32(DUNITS * zoom * (l.x + l.width)), Convert.ToInt32(DUNITS * zoom * (l.y)));
                        p2 = new Point(Convert.ToInt32((DUNITS * zoom * (l.x + l.width)) - arrowWidth), Convert.ToInt32((DUNITS * zoom * (l.y)) + (arrowHeight / 2)));
                        p3 = new Point(Convert.ToInt32((DUNITS * zoom * (l.x + l.width)) - arrowWidth), Convert.ToInt32((DUNITS * zoom * (l.y)) - (arrowHeight / 2)));
                    }
                    else
                    {
                        p1 = new Point(Convert.ToInt32(DUNITS * zoom * (l.x)), Convert.ToInt32(DUNITS * zoom * (l.y + l.height)));
                        p2 = new Point(Convert.ToInt32((DUNITS * zoom * (l.x)) - (arrowHeight / 2)), Convert.ToInt32((DUNITS * zoom * (l.y + l.height)) - (arrowWidth)));
                        p3 = new Point(Convert.ToInt32((DUNITS * zoom * (l.x)) + (arrowHeight / 2)), Convert.ToInt32((DUNITS * zoom * (l.y + l.height) - (arrowWidth))));

                    }
                    dwgSheet.DrawLine(thisPen, p1, p2);
                    dwgSheet.DrawLine(thisPen, p1, p3);
                    dwgSheet.DrawLine(thisPen, p2, p3);
                }

                lineCount++;
            }
        }


        public void linesToExcel()
        {
            this.Cursor = Cursors.WaitCursor;
            //start excel
            NsExcel.Application excapp = new Microsoft.Office.Interop.Excel.Application();

            //if you want to make excel visible           
            excapp.Visible = true;

            //create a blank workbook
            NsExcel.Workbook workbook = excapp.Workbooks.Add(NsExcel.XlWBATemplate.xlWBATWorksheet);

            //Not done yet. You have to work on a specific sheet - note the cast
            //You may not have any sheets at all. Then you have to add one with NsExcel.Worksheet.Add()
            var sheet = (NsExcel.Worksheet)workbook.Sheets[1]; //indexing starts from 1

            //now the list
            int row = 1;
            int c = 1;

            sheet.Cells[row, c++] = "GLYPH";
            sheet.Cells[row, c++] = "X";
            sheet.Cells[row, c++] = "Y";
            sheet.Cells[row, c++] = "WIDTH";
            sheet.Cells[row, c++] = "HEIGHT";
            sheet.Cells[row, c++] = "TYPE";
            sheet.Cells[row, c++] = "ATTR1";
            sheet.Cells[row, c++] = "ATTR2";
            sheet.Cells[row, c++] = "ATTR3";
            sheet.Cells[row, c++] = "ATTR4";
            sheet.Cells[row, c++] = "ATTR5";
            row++;

            foreach (LineElement l in lines)
            {
                c = 1;
                sheet.Cells[row, c++] = l.glyph;
                sheet.Cells[row, c++] = l.x;
                sheet.Cells[row, c++] = l.y;
                sheet.Cells[row, c++] = l.width;
                sheet.Cells[row, c++] = l.height;
                sheet.Cells[row, c++] = l.type;
                sheet.Cells[row, c++] = l.attr[0];
                sheet.Cells[row, c++] = l.attr[1];
                sheet.Cells[row, c++] = l.attr[2];
                sheet.Cells[row, c++] = l.attr[3];
                row++;
            }
            this.Cursor = Cursors.Default;
        }

        private void dText(Graphics dwgSheet)
        {
            foreach (TextElement t in text)
            {
                int xoffset = 0;
                int yoffset = 0;
                Brush thisBrush = Brushes.Black;
                Pen thisPen = new Pen(Color.Black, 1 * zoom);
                if (searchText != null)
                {
                    if (Regex.IsMatch(t.text, searchText, RegexOptions.IgnoreCase))
                    {
                        thisBrush = Brushes.Lime;
                        thisPen.Color = Color.Lime;
                    }
                }
                byte textHeight = (byte)((t.font & TEXT_FONT_SIZE) >> 5);
                if ((textHeight <= 5) && (textHeight > 0))
                { // 6 appears as smallest text (no print)


                    Font thisFont = new Font("Arial", 0.5F * DUNITS * zoom * (textHeight + 2));
                    StringFormat stringFormat = new StringFormat();
                    dwgSheet.TranslateTransform(Convert.ToInt32(DUNITS * zoom * t.x), Convert.ToInt32(DUNITS * zoom * t.y));
                    if ((t.font & TEXT_FONT_VERTICAL) > 0)
                    {
                        dwgSheet.RotateTransform(-90);
                        xoffset = -t.height;
                        yoffset = 0;
                    }

                    if ((t.style & TEXT_STYLE_HIDE) == 0)
                    {
                        dwgSheet.DrawString(t.text, thisFont, thisBrush,
                            new Point(Convert.ToInt32(DUNITS * zoom * xoffset), Convert.ToInt32(DUNITS * zoom * yoffset)), stringFormat);

                        if ((t.style & TEXT_STYLE_OVERSCORE) > 0)
                        {
                            dwgSheet.DrawLine(thisPen,
                                new Point(Convert.ToInt32(DUNITS * zoom * xoffset), Convert.ToInt32(DUNITS * zoom * yoffset)),
                                new Point(Convert.ToInt32(DUNITS * zoom * (xoffset + t.width)), Convert.ToInt32(DUNITS * zoom * (yoffset))));

                        }
                    }
                    dwgSheet.ResetTransform();
                }
            }
        }

        public void textToExcel()
        {
            this.Cursor = Cursors.WaitCursor;
            //start excel
            NsExcel.Application excapp = new Microsoft.Office.Interop.Excel.Application();

            //if you want to make excel visible           
            excapp.Visible = true;

            //create a blank workbook
            NsExcel.Workbook workbook = excapp.Workbooks.Add(NsExcel.XlWBATemplate.xlWBATWorksheet);

            //Not done yet. You have to work on a specific sheet - note the cast
            //You may not have any sheets at all. Then you have to add one with NsExcel.Worksheet.Add()
            var sheet = (NsExcel.Worksheet)workbook.Sheets[1]; //indexing starts from 1

            //now the list
            int row = 1;
            int c = 1;

            sheet.Cells[row, c++] = "TEXT";
            sheet.Cells[row, c++] = "LAYER";
            sheet.Cells[row, c++] = "X";
            sheet.Cells[row, c++] = "Y";
            sheet.Cells[row, c++] = "WIDTH";
            sheet.Cells[row, c++] = "HEIGHT";
            sheet.Cells[row, c++] = "FONT";
            sheet.Cells[row, c++] = "STYLE";
            sheet.Cells[row, c++] = "FLAGS3";
            sheet.Cells[row, c++] = "FLAGS4";
            sheet.Cells[row, c++] = "PAD";

            row++;
            foreach (TextElement t in text)
            {
                c = 1;
                sheet.Cells[row, c++] = t.text;
                sheet.Cells[row, c++] = t.layer;
                sheet.Cells[row, c++] = t.x;
                sheet.Cells[row, c++] = t.y;
                sheet.Cells[row, c++] = t.width;
                sheet.Cells[row, c++] = t.height;
                sheet.Cells[row, c++] = t.font;
                sheet.Cells[row, c++] = t.style;
                sheet.Cells[row, c++] = t.flags[0];
                sheet.Cells[row, c++] = t.flags[1];
                sheet.Cells[row, c++] = t.pad;
                row++;
            }
            this.Cursor = Cursors.Default;
        }

        private void dSymbols(Graphics dwgSheet)
        {
            foreach (SymbolElement s in symbols)
            {
                Color thisColor = Color.Black;
                if (s.id.ToString() == symbolCode)
                {
                    thisColor = Color.Lime;
                }
                Rectangle r = new Rectangle(Convert.ToInt32(DUNITS * zoom * s.x),
                                            Convert.ToInt32(DUNITS * zoom * s.y),
                                            Convert.ToInt32(DUNITS * zoom * s.width),
                                            Convert.ToInt32(DUNITS * zoom * s.height));


                if (showSymbolID)
                {
                    Brush thisBrush = Brushes.Red;
                    Font thisFont = new Font("Arial", 0.5F * DUNITS * zoom * 2);
                    dwgSheet.DrawString(s.id.ToString(), thisFont, thisBrush,
        new Point(Convert.ToInt32(DUNITS * zoom * s.x), Convert.ToInt32(DUNITS * zoom * s.y)));
                }
                Symbol thisSymbol = new Symbol(dwgSheet, elementCollection, thisColor, new Point(s.x, s.y), new Size(s.width, s.height), zoom, s.transform, DUNITS, reportStr);
                thisSymbol.draw(s.bytes2);
            }
        }

        private Size GetDWGExtents()
        {
            return (new Size(Convert.ToInt32(header.width * DUNITS * zoom / 4), Convert.ToInt32(header.height * DUNITS * zoom / 4)));
        }

        public void symbolsToExcel()
        {
            this.Cursor = Cursors.WaitCursor;
            //start excel
            NsExcel.Application excapp = new Microsoft.Office.Interop.Excel.Application();

            //if you want to make excel visib   le           
            excapp.Visible = true;

            //create a blank workbook
            NsExcel.Workbook workbook = excapp.Workbooks.Add(NsExcel.XlWBATemplate.xlWBATWorksheet);

            //Not done yet. You have to work on a specific sheet - note the cast
            //You may not have any sheets at all. Then you have to add one with NsExcel.Worksheet.Add()
            var sheet = (NsExcel.Worksheet)workbook.Sheets[1]; //indexing starts from 1

            //now the list
            int row = 1;
            int c = 1;

            sheet.Cells[row, c++] = "LAYER";
            sheet.Cells[row, c++] = "X";
            sheet.Cells[row, c++] = "Y";
            sheet.Cells[row, c++] = "WIDTH";
            sheet.Cells[row, c++] = "HEIGHT";
            sheet.Cells[row, c++] = "ID";
            sheet.Cells[row, c++] = "LENGTH";
            sheet.Cells[row, c++] = "TRANSFORM";

            for (int i = 0; i < 16; i++)
            {
                sheet.Cells[row, c++] = "BYTE" + i.ToString("0#");
            }

            row++;
            foreach (SymbolElement s in symbols)
            {
                c = 1;
                sheet.Cells[row, c++] = s.layer;
                sheet.Cells[row, c++] = s.x;
                sheet.Cells[row, c++] = s.y;
                sheet.Cells[row, c++] = s.width;
                sheet.Cells[row, c++] = s.height;
                sheet.Cells[row, c++] = s.id; // may be ID
                sheet.Cells[row, c++] = s.length; // symbol length in bytes
                sheet.Cells[row, c++] = s.transform;
                for (int i = 0; i < s.length; i++)
                {
                    sheet.Cells[row, c++] = (byte)s.bytes2[i];
                }
                row++;
            }
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Size limits = GetDWGExtents();
            Graphics dwgSheet = pictureBox1.CreateGraphics();
            dwgSheet.Clear(Color.WhiteSmoke);
            dSymbols(dwgSheet);
            dLines(dwgSheet);
            dText(dwgSheet);
            this.Cursor = Cursors.Default;
        }

        public void findLine()
        {
            FindLine f = new FindLine();
            f.LinePoint = linePoint;
            if (f.ShowDialog() == DialogResult.OK)
            {
                linePoint = f.LinePoint;
                pictureBox1.Refresh();
            }
        }

        public void findText()
        {
            FindText f = new FindText();
            f.SearchText = searchText;
            if (f.ShowDialog() == DialogResult.OK)
            {
                searchText = f.SearchText;
                pictureBox1.Refresh();
            }
        }

        public void exportAsPNG()
        {


            string pngFile = "";
            if (fileName != "")
            {
                pngFile = Regex.Replace(fileName, ".dwg\\z", ".png", RegexOptions.IgnoreCase); ;
            }
            saveFileDialog1.FileName = pngFile;
            saveFileDialog1.Filter = "PNG Files|*.png";
            saveFileDialog1.Title = "Save PNG File";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    pngFile = saveFileDialog1.FileName;
                    Bitmap bm = getImage();
                    bm.Save(pngFile, ImageFormat.Png);

                    MessageBox.Show("Image saved to " + pngFile, "Image Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving PNG: " + ex.Message, "Save PNG Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void exportAsPDF()
        {
            string pdfFile = "";
            if (fileName != "")
            {
                pdfFile = Regex.Replace(fileName, ".dwg\\z", ".pdf", RegexOptions.IgnoreCase); ;
            }
            saveFileDialog1.FileName = pdfFile;
            saveFileDialog1.Filter = "PDF Files|*.pdf";
            saveFileDialog1.Title = "Save PDF File";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Bitmap bm = getImage();
                    float resolution = (bm.HorizontalResolution / 25.4F);

                    XImage img = XImage.FromGdiPlusImage(bm);
                    PdfDocument doc = new PdfDocument();
                    PdfPage page = doc.Pages.Add(new PdfPage());
                    page.Width = XUnit.FromPoint(img.Size.Width) + 4;
                    page.Height = XUnit.FromPoint(img.Size.Height) + 4;
                    XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);

                    xgr.DrawImage(img, 0, 0);
                    doc.Save(pdfFile);
                    doc.Close();

                    Process.Start(pdfFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving PDF: " + ex.Message, "Save PDF Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public Bitmap getImage()
        {
            float saveZoom = zoom;
            saveZoom = zoom;
            zoom = 1;

            Size limits = GetDWGExtents();
            limits.Width += 1;
            limits.Height += 1;
            limits.Width *= 4;
            limits.Height *= 4;

            int width = limits.Width;
            int height = limits.Height;
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            dLines(g);
            dText(g);
            dSymbols(g);
            zoom = saveZoom;
            return (bm);
        }


        public void loadElements()
        {
            elementCollection.Clear();
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string allText = (string)Properties.Resources.ResourceManager.GetObject("elements");

                string[] lines = allText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string l in lines)
                {
                    string[] sa = l.Split(',');
                    byte[] ba = new byte[sa.Count()];
                    int idx = 0;
                    foreach (string s in sa)
                    {
                        if (s != null && s != "")
                        {
                            ba[idx] = Convert.ToByte(s);
                            idx++;
                        }

                    }
                    elementCollection.Add(ba);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error reading elements file: " + e.Message, "Elements File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public void showSymbolIDs(object sender)
        {
            ToolStripMenuItem t = sender as ToolStripMenuItem;
            if (!t.Checked)
            {
                t.Checked = true;
                showSymbolID = true;
            }
            else
            {
                t.Checked = false;
                showSymbolID = false;
            }
            pictureBox1.Refresh();
        }

        public void findSymbol()
        {
            FindSymbol f = new FindSymbol();
            f.SymbolNumber.Text = symbolCode;
            if (f.ShowDialog() == DialogResult.OK)
            {
                symbolCode = f.SymbolNumber.Text;
                pictureBox1.Refresh();
            }
        }

        public void zoomDrawing(object sender)
        {
            ToolStripMenuItem t = sender as ToolStripMenuItem;
            zoom = Convert.ToInt32(t.Tag.ToString()) / 100F;
            LoadFile();
        }
    }
}
