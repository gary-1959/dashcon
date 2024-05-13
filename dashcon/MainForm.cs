using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using PdfSharp.Drawing;
using PdfSharp.Pdf;


namespace dashcon
{

    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            tabControl1.TabPages.Clear();

            foreach (string f in Properties.Settings.Default.fileNames)
            {
                addTab(f);
            }

        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string f in files)
            {
                addTab(f);
            }
            saveFileNames();
        }

        private XtraTabPage addTab(string f)
        {
            foreach (XtraTabPage t in tabControl1.TabPages)
            {
                FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
                if (v.FileName == f) return(t);
            }
            if (File.Exists(f))
            {
                XtraTabPage t = new XtraTabPage();
                t.AutoScroll = true;
                FileViewer v = new FileViewer();
                v.Name = "fileViewer1";
                v.FileName = f;
                t.Controls.Add(v);
                t.Text = f;
                tabControl1.TabPages.Add(t);
                return (t);
            }
            return (null);
        }

        private void setTitle()
        {
            this.Text = "DASHCON Dash File Converter";
        }

        private void refreshPage_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.Refresh();
        }

        private void findLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.findLine();
        }

        private void findTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.findText();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.LoadFile();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XtraTabPage thisTab = null;
            openFileDialog1.Filter = "DASH Files|*.dwg";
            openFileDialog1.Title = "Select DASH File(s)";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string f in openFileDialog1.FileNames)
                {
                    thisTab = addTab(f);
                    if (thisTab != null)
                    {
                        tabControl1.SelectedTabPage = thisTab;
                    }
                }
                saveFileNames();
            }
        }

        private void saveFileNames()
        {
            Properties.Settings.Default.fileNames.Clear();
            foreach (XtraTabPage t in tabControl1.TabPages)
            {
                FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
                Properties.Settings.Default.fileNames.Add(v.FileName);
            }
            Properties.Settings.Default.Save();
        }

        private void exportAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.exportAsPNG();
        }

        private void exportAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.exportAsPDF();
        }


        private void showSymbolIDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage t in tabControl1.TabPages)
            {
                FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
                v.showSymbolIDs(sender);
            }
        }

        private void contrelecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.contrelec.co.uk");
        }

        private void aboutDASHCONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new AboutWindow();
            f.ShowDialog();
        }

        private void findSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.findSymbol();
        }

        private void zoom10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.zoomDrawing(sender);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileNames();
            this.Close();
        }

        private void linesToExcel_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.linesToExcel();
        }

        private void symbolsToExcel_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.symbolsToExcel();
        }

        private void textToExcel_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            v.textToExcel();
        }

        private void tabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
            string f = v.FileName;

            Properties.Settings.Default.fileNames.Remove(v.FileName);
            Properties.Settings.Default.Save();

            tabControl1.TabPages.Remove(tabControl1.SelectedTabPage);
        }

        private void exportAllToPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pdfFile = "";
            saveFileDialog1.Filter = "PDF Files|*.pdf";
            saveFileDialog1.Title = "Save PDF File";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    pdfFile = saveFileDialog1.FileName;
                    PdfDocument doc = new PdfDocument();
                    foreach (XtraTabPage t in tabControl1.TabPages)
                    {
                        FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;
                        Bitmap bm = v.getImage();
                        float resolution = (bm.HorizontalResolution / 25.4F);
                        XImage img = XImage.FromGdiPlusImage(bm);
                        PdfPage page = doc.Pages.Add(new PdfPage());
                        page.Width = XUnit.FromPoint(img.Size.Width) + 4;
                        page.Height = XUnit.FromPoint(img.Size.Height) + 4;
                        XGraphics xgr = XGraphics.FromPdfPage(page);
                        xgr.DrawImage(img, 0, 0);
                        xgr.Dispose();
                        img.Dispose();
                        bm.Dispose();

                    }
                    doc.Save(pdfFile);
                    doc.Close();
                    this.Cursor = Cursors.Default;

                    Process.Start(pdfFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving PDF: " + ex.Message, "Save PDF Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        Point p = Point.Empty;
        XtraTabPage page = null;

        private void tabControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            XtraTabControl c = sender as XtraTabControl;
            p = new Point(e.X, e.Y);
            XtraTabHitInfo hi = c.CalcHitInfo(p);
            page = hi.Page;
            if (hi.Page == null)
                p = Point.Empty;
        }

        private void tabControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if ((p != Point.Empty) && ((Math.Abs(e.X - p.X) > SystemInformation.DragSize.Width) || (Math.Abs(e.Y - p.Y) > SystemInformation.DragSize.Height)))
                    tabControl1.DoDragDrop(sender, DragDropEffects.Move);
        }

        private void tabControl1_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            XtraTabControl c = sender as XtraTabControl;
            if (c == null)
                return;
            XtraTabHitInfo hi = c.CalcHitInfo(c.PointToClient(new Point(e.X, e.Y)));
            if (hi.Page != null)
            {
                if (hi.Page != page)
                {
                    if (c.TabPages.IndexOf(hi.Page) < c.TabPages.IndexOf(page))
                        c.TabPages.Move(c.TabPages.IndexOf(hi.Page), page);
                    else
                        c.TabPages.Move(c.TabPages.IndexOf(hi.Page) + 1, page);
                }
                e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void viewConversionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == null) return;
            XtraTabPage t = tabControl1.SelectedTabPage;
            FileViewer v = t.Controls.Find("fileViewer1", false)[0] as FileViewer;

            v.viewReport();
        }
    }
}
