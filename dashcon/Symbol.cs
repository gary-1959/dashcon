using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace dashcon
{
    class Symbol
    {
        Point P;    // current relative co-ordinates
        Point L;    // location point
        Color C;
        float S;
        Size R;     // size
        Graphics G, Target;
        int D;
        Bitmap thisBMP;
        byte transform;
        string report;
        List<byte[]> elementCollection;

        public Symbol(Graphics g, List<byte[]> el, Color c, Point l, Size r, float s, byte t, int dunits, string rpt)
        {
            D = dunits;
            C = c;
            L = new Point(Convert.ToInt32(l.X * D * s), Convert.ToInt32(l.Y * D * s));
            P = new Point(0, 0);
            S = s;
            R = new Size(Convert.ToInt32(r.Width * D * s), Convert.ToInt32(r.Height * D * s));
            Target = g;
            transform = t;
            report = rpt;
            elementCollection = el;

            thisBMP = new Bitmap((int)(R.Width + 1), (int)(R.Height + 1));
            G = Graphics.FromImage(thisBMP);

        }

        public void draw(sbyte[] dbytes)
        {

 
            int idx = 0;
            sbyte px, py;
            do
            {
                byte cmd = (byte)(dbytes[idx]);
                if (cmd == 0xff) break;
                if ((cmd & 0x80) == 0)  // short commands
                {
                    switch (cmd & 0xe0)
                    {
                        case (0x00):
                            idx += DXS(cmd);
                            break;

                        case (0x20):
                            idx += DYS(cmd);
                            break;

                        case (0x40):
                            idx += MXS(cmd);
                            break;

                        case (0x60):
                            idx += MYS(cmd);
                            break;
                        default:
                            report += "Unrecognised short symbol command (" + cmd + ")\n";
                            idx += 1;
                            break;
                    }
                }
                else
                {
                    switch (cmd)
                    {

                        case (0xC0):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            idx += DX(px);
                            break;
                        case (0xC1):
                            py = Convert.ToSByte(dbytes[idx + 1]);
                            idx += DY(py);
                            break;
                        case (0xC2):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += DXY(px, py);
                            break;
                        case (0xC3):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += DR(px, py);
                            break;
                        case (0xC4):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            idx += MX(px);
                            break;
                        case (0xC5):
                            py = Convert.ToSByte(dbytes[idx + 1]);
                            idx += MY(py);
                            break;
                        case (0xC6):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += MXY(px, py);
                            break;

                        case (0xC7):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            idx += BX(px);
                            break;
                        case (0xC8):
                            py = Convert.ToSByte(dbytes[idx + 1]);
                            idx += BY(py);
                            break;

                        case (0xC9):
                            idx += IG(Convert.ToByte(dbytes[idx + 1]));
                            break;


                        case (0xCA):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += ID(px, py);
                            break;

                        case (0xCB):
                            idx += ISC((byte)dbytes[idx + 1]);
                            break;


                        case (0xCC):
                            idx += DXL();
                            break;
                        case (0xCD):
                            idx += DXR();
                            break;
                        case (0xCE):
                            idx += DYU();
                            break;
                        case (0xCF):
                            idx += DYL();
                            break;

                        case (0xD0):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            idx += DXD(px);
                            break;
                        case (0xD1):
                            py = Convert.ToSByte(dbytes[idx + 1]);
                            idx += DYD(py);
                            break;
                        case (0xD2):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += DXYD(px, py);
                            break;
                        case (0xD3):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += DRD(px, py);
                            break;
                        case (0xD4):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            idx += MXD(px);
                            break;
                        case (0xD5):
                            py = Convert.ToSByte(dbytes[idx + 1]);
                            idx += MYD(py);
                            break;
                        case (0xD6):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += MXYD(px, py);
                            break;
                        case (0xDC):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += MXYA(px, py);
                            break;

                        case (0xE0):
                            idx += ISTC((byte)dbytes[idx + 1]);
                            break;
                        case (0xE1):
                            idx += ISTR((byte)dbytes[idx + 1]);
                            break;
                        case (0xE2):
                            idx += ISRC((byte)dbytes[idx + 1]);
                            break;
                        case (0xE3):
                            idx += ISBR((byte)dbytes[idx + 1]);
                            break;
                        case (0xE4):
                            idx += ISBC((byte)dbytes[idx + 1]);
                            break;
                        case (0xE5):
                            idx += ISBL((byte)dbytes[idx + 1]);
                            break;
                        case (0xE6):
                            idx += ISLC((byte)dbytes[idx + 1]);
                            break;
                        case (0xE7):
                            idx += ISTL((byte)dbytes[idx + 1]);
                            break;

                        case (0xE8):
                            idx += BXL();
                            break;
                        case (0xE9):
                            idx += BXR();
                            break;
                        case (0xEA):
                            idx += BYU();
                            break;
                        case (0xEB):
                            idx += BYL();
                            break;
                        case (0xED):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            idx += DC(px);
                            break;
                        case (0xEE):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            idx += DCD(px);
                            break;
                        case (0xEF):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += DA(px, Convert.ToSByte((py & 0xf0) >> 4), Convert.ToSByte(py & 0x0f));
                            break;
                        case (0xF0):
                            px = Convert.ToSByte(dbytes[idx + 1]);
                            py = Convert.ToSByte(dbytes[idx + 2]);
                            idx += DAD(px, Convert.ToSByte((py & 0xf0) >> 4), Convert.ToSByte(py & 0x0f));
                            break;
                        default:
                            report += "Unrecognised symbol command (" + cmd + ")\n";
                            idx += 1;
                            break;
                    }

                }


            } while (true);


            if ((transform & 0x08) > 0)
            {
                thisBMP.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }

            if ((transform & 0x02) > 0)
            {
                thisBMP.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            if ((transform & 0x04) > 0)
            {
                thisBMP.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }

            Target.DrawImage(thisBMP, L.X, L.Y);
        }


        private void line(Pen p, Point p1, Point p2)
        {

            G.DrawLine(p, new Point(Convert.ToInt32(p1.X), Convert.ToInt32(p1.Y)),
                     new Point(Convert.ToInt32(p2.X), Convert.ToInt32(p2.Y)));

        }

        private void rectangle(Pen p, Point p1, Size p2)
        {
            G.DrawRectangle(p, p1.X, p1.Y, p2.Width, p2.Height);
        }

        private void circle(Pen p, Point p1, int r)
        {
            G.DrawEllipse(p, (p1.X - r), (p1.Y - r), r * 2, r * 2);
        }

        private void ibitmap(Point p1, Image img)
        {
            Rectangle ir = new Rectangle(p1.X, p1.Y, Convert.ToInt32(img.Width * S), Convert.ToInt32(img.Height * S));
            //G.DrawRectangle(new Pen(Color.Magenta, 1), ir);
            //img.MakeTransparent(Color.White);
            G.DrawImage(img, ir);
        }


        private Point arc(Pen p, Point p1, int r, int s, int f)
        {

            // translate rectangle so that centre is at starting point

            Rectangle rect = new Rectangle(p1.X, p1.Y, 2 * r, 2 * r);
            rect.X -= r;
            rect.Y -= r;

            // translate arc so that start point is at p1
            // arc drawing routine has 0 at 3 o'clock, DASH at 6 o'clock
            rect.X = Convert.ToInt32(rect.X + (r * sin(s - 90)));
            rect.Y = Convert.ToInt32(rect.Y - (r * cos(s - 90)));
            G.DrawArc(p, rect, s, f);

            // get finishing point
            return (new Point(Convert.ToInt32(rect.X + r - (r * sin(s + f - 90))), Convert.ToInt32(rect.Y + r + (r * cos(s + f - 90)))));
        }

        // degrees version of trig functions
        double sin(double a)
        {
            return (Math.Sin(a * Math.PI / 180));
        }

        double cos(double a)
        {
            return (Math.Cos(a * Math.PI / 180));
        }

        private int DXS(byte cmd)
        {
            Pen p = new Pen(C, 1 * S);
            int x = cmd & 0x0f;
            if ((cmd & 0x10) > 0) x -= 16;
            line(p, P, new Point(Convert.ToInt32(P.X + (x * S * D)), P.Y));
            P.X += Convert.ToInt32(x * S * D);
            return (1);
        }

        private int DYS(byte cmd)
        {
            Pen p = new Pen(C, 1 * S);
            int y = cmd & 0x0f;
            if ((cmd & 0x10) > 0) y -= 16;
            line(p, P, new Point(P.X, Convert.ToInt32(P.Y + (y * S * D))));
            P.Y += Convert.ToInt32(y * S * D);
            return (1);
        }


        private int MXS(byte cmd)
        {
            Pen p = new Pen(C, 1 * S);
            int x = cmd & 0x0f;
            if ((cmd & 0x10) > 0) x -= 16;
            P.X += Convert.ToInt32(x * S * D);
            return (1);
        }

        private int MYS(byte cmd)
        {
            Pen p = new Pen(C, 1 * S);
            int y = cmd & 0x0f;
            if ((cmd & 0x10) > 0) y -= 16;
            P.Y += Convert.ToInt32(y * S * D);
            return (1);
        }

        private int BXL()
        {
            Pen p = new Pen(C, 1 * S);
            float r = 0.5F;
            circle(p, new Point(Convert.ToInt32(P.X - (r * S)), P.Y), Convert.ToInt32(r * S));
            line(p, new Point(Convert.ToInt32(P.X - (2 * r * S)), P.Y), new Point(0, P.Y));
            return (1);
        }

        private int BXR()
        {
            Pen p = new Pen(C, 1 * S);
            float r = 0.5F;
            circle(p, new Point(Convert.ToInt32(P.X + (r * S)), P.Y), Convert.ToInt32(r * S));
            line(p, new Point(Convert.ToInt32(P.X + (2 * r * S)), P.Y), new Point(R.Width, P.Y));
            return (1);
        }

        private int BYU()
        {
            Pen p = new Pen(C, 1 * S);
            float r = 0.5F;
            circle(p, new Point(P.X, Convert.ToInt32(P.Y - (r * S))), Convert.ToInt32(r * S));
            line(p, new Point(P.X, Convert.ToInt32(P.Y - (2 * r * S))), new Point(P.X, 0));
            return (1);
        }

        private int BYL()
        {
            Pen p = new Pen(C, 1 * S);
            float r = 0.5F;
            circle(p, new Point(P.X, Convert.ToInt32(P.Y + (r * S))), Convert.ToInt32(r * S));
            line(p, new Point(P.X, Convert.ToInt32(P.Y + (2 * r * S))), new Point(P.X, R.Height));
            return (1);
        }

        private int DX(sbyte x)
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(Convert.ToInt32(P.X + (x * S * D)), P.Y));
            P.X += Convert.ToInt32(x * S * D);
            return (2);
        }

        private int DY(sbyte y)
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(P.X, Convert.ToInt32(P.Y + (y * S * D))));
            P.Y += Convert.ToInt32(y * S * D);
            return (2);
        }

        private int DXY(sbyte x, sbyte y)
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(Convert.ToInt32(P.X + (x * S * D)), Convert.ToInt32(P.Y + (y * S * D))));
            P.X += Convert.ToInt32(x * S * D);
            P.Y += Convert.ToInt32(y * S * D);
            return (3);
        }

        private int BX(sbyte x)
        {
            Pen p = new Pen(C, 1 * S);
            float[] dashValues = { 1 * S, 1 * S };
            p.DashPattern = dashValues;
            line(p, P, new Point(Convert.ToInt32(P.X + (x * S * D)), P.Y));
            P.X += Convert.ToInt32(x * S * D);
            return (2);
        }

        private int BY(sbyte y)
        {
            Pen p = new Pen(C, 1 * S);
            float[] dashValues = { 1 * S, 1 * S };
            p.DashPattern = dashValues;
            line(p, P, new Point(P.X, Convert.ToInt32(P.Y + (y * S * D))));
            P.Y += Convert.ToInt32(y * S * D);
            return (2);
        }

        private int DXL()
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(0, P.Y));
            return (1);
        }

        private int DXR()
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(R.Width, P.Y));
            return (1);
        }

        private int DYU()
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(P.X, 0));
            return (1);
        }

        private int DYL()
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(P.X, R.Height));
            return (1);
        }

        private int DXD(sbyte x)
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(Convert.ToInt32(P.X + (x * S)), P.Y));
            P.X += Convert.ToInt32(x * S);
            return (2);
        }

        private int DYD(sbyte y)
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(P.X, Convert.ToInt32(P.Y + (y * S))));
            P.Y += Convert.ToInt32(y * S);
            return (2);
        }

        private int DXYD(sbyte x, sbyte y)
        {
            Pen p = new Pen(C, 1 * S);
            line(p, P, new Point(Convert.ToInt32(P.X + (x * S)), Convert.ToInt32(P.Y + (y * S))));
            P.X += Convert.ToInt32(x * S);
            P.Y += Convert.ToInt32(y * S);
            return (3);
        }

        private int MX(sbyte x)
        {
            P.X += Convert.ToInt32(x * S * D);
            return (2);
        }

        private int MY(sbyte y)
        {
            P.Y += Convert.ToInt32(y * S * D);
            return (2);
        }

        private int MXY(sbyte x, sbyte y)
        {
            P.X += Convert.ToInt32(x * S * D);
            P.Y += Convert.ToInt32(y * S * D);
            return (3);
        }

        private int MXD(sbyte x)
        {
            P.X += Convert.ToInt32(x * S);
            return (2);
        }

        private int MYD(sbyte y)
        {
            P.Y += Convert.ToInt32(y * S);
            return (2);
        }

        private int MXYD(sbyte x, sbyte y)
        {
            P.X += Convert.ToInt32(x * S);
            P.Y += Convert.ToInt32(y * S);
            return (3);
        }

        private int MXYA(sbyte x, sbyte y)
        {
            P.X = Convert.ToInt32(x * S * D);
            P.Y = Convert.ToInt32(y * S * D);
            return (3);
        }

        private int DR(sbyte w, sbyte h)
        {
            Pen p = new Pen(C, 1 * S);
            Point s = P;
            if (w < 0)
            {
                w = Convert.ToSByte(-w);
                s.X = s.X - Convert.ToInt32(w * S * D);
            }

            if (h < 0)
            {
                h = Convert.ToSByte(-h);
                s.Y = s.Y - Convert.ToInt32(h * S * D);
            }
            rectangle(p, s, new Size(Convert.ToInt32(w * S * D), Convert.ToInt32(h * S * D)));
            return (3);
        }

        private int DRD(sbyte w, sbyte h)
        {
            Pen p = new Pen(C, 1 * S);
            Point s = P;
            if (w < 0)
            {
                w = Convert.ToSByte(-w);
                s.X = s.X - Convert.ToInt32(w * S);
            }

            if (h < 0)
            {
                h = Convert.ToSByte(-h);
                s.Y = s.Y - Convert.ToInt32(h * S);
            }
            rectangle(p, s, new Size(Convert.ToInt32(w * S), Convert.ToInt32(h * S)));
            return (3);
        }

        private int DC(sbyte r)
        {
            Pen p = new Pen(C, 1 * S);
            circle(p, P, Convert.ToInt32(r * S * D));
            return (2);
        }

        private int DCD(sbyte r)
        {
            Pen p = new Pen(C, 1 * S);
            circle(p, P, Convert.ToInt32(r * S));
            return (2);
        }

        private int DA(sbyte r, sbyte s, sbyte f)
        {
            Pen p = new Pen(C, 1 * S);
            // start angle is supplied as 8 segments with 0 at 6 o'clock
            // required start angle is 0 on the x-axis in degrees increasing clockwise
            float sa = 90 + (s * 45);
            sa = (sa >= 360 ? sa - 360 : sa);
            // sweep angle is supplied as +/ 8 segments
            // zero does not exist, so 0 = 1 so values representing -8 to +8 are possible with 4 bits
            // required is angle in degrees

            if ((f & 0x08) > 0)
            {
                f = Convert.ToSByte(f & 0x07);
                f = (sbyte)(-f - 1);
            }
            else
            {
                f++;
            }

            float sw = (f * 45);

            P = arc(p, P, Convert.ToInt32(r * S * D), Convert.ToInt32(sa), Convert.ToInt32(sw));
            return (3);
        }
        private int DAD(sbyte r, sbyte s, sbyte f)
        {
            Pen p = new Pen(C, 1 * S);
            // start angle is supplied as 8 segments with 0 at 6 o'clock
            // required start angle is 0 on the x-axis in degrees increasing clockwise
            float sa = 90 + (s * 45);
            sa = (sa >= 360 ? sa - 360 : sa);
            // sweep angle is supplied as +/ 8 segments
            // zero does not exist, so 0 = 1 so values representing -8 to +8 are possible with 4 bits
            // required is angle in degrees

            if ((f & 0x08) > 0)
            {
                f = Convert.ToSByte(f & 0x07);
                f = (sbyte)(-f - 1);
            }
            else
            {
                f++;
            }

            float sw = (f * 45);

            P = arc(p, P, Convert.ToInt32(r * S), Convert.ToInt32(sa), Convert.ToInt32(sw));
            return (3);
        }

        private int ID(sbyte a, sbyte b)
        {
            SolidBrush aBrush = new SolidBrush(C);
            sbyte[] bg = { (sbyte)(a & 0xf0 >> 4), (sbyte)(a & 0x0f), (sbyte)(b & 0xf0 >> 4), (sbyte)(b & 0x0f) };
            for (int i = 0; i < 4; i++)
            {
                sbyte mask = 0x08;
                for (int j = 0; j < 4; j++)
                {
                    if ((mask & bg[i]) > 0)
                    {
                        G.FillRectangle(aBrush, P.X + (j * S), P.Y + (i * S), (1 * S), (1 * S));
                    }
                    mask = (sbyte)(mask >> 1);
                }
            }
            return (3);

        }

        private int IG(byte el)
        {
            Point savePoint = P;
            foreach (byte[] element in elementCollection)
            {
                if (element[0] == el)
                {
                    sbyte[] sb = new sbyte[element.Count()];

                    for (int i = 1; i < element.Count(); i++)
                    {
                        sb[i - 1] = (sbyte)element[i];
                    }
                    draw(sb);
                }
            }
            P = savePoint;
            return (2);
        }

        private int ISC(byte el)
        {
            loadBitMap(el, 'c', 'c');
            return (2);
        }

        private int ISBC(byte el)
        {
            loadBitMap(el, 'c', 'b');
            return (2);
        }

        private int ISBR(byte el)
        {
            loadBitMap(el, 'r', 'b');
            return (2);
        }

        private int ISBL(byte el)
        {
            loadBitMap(el, 'l', 'b');
            return (2);
        }

        private int ISLC(byte el)
        {
            loadBitMap(el, 'l', 'c');
            return (2);
        }

        private int ISRC(byte el)
        {
            loadBitMap(el, 'r', 'c');
            return (2);
        }

        private int ISTC(byte el)
        {
            loadBitMap(el, 'c', 't');
            return (2);
        }

        private int ISTR(byte el)
        {
            loadBitMap(el, 'r', 't');
            return (2);
        }

        private int ISTL(byte el)
        {
            loadBitMap(el, 'l', 't');
            return (2);
        }

        private void loadBitMap(byte el, char posX, char posY)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                    Bitmap image1 = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + el.ToString());

                    float xOffset = 0;
                    float yOffset = 0;

                    switch (posX)
                    {
                        case 'l':
                            xOffset = 0;
                            break;
                        case 'c':
                            xOffset = -image1.Width / 2;
                            break;
                        case 'r':
                            xOffset = -image1.Width;
                            break;
                    }

                    switch (posY)
                    {
                        case 't':
                            yOffset = 0;
                            break;
                        case 'c':
                            yOffset = -image1.Height / 2;
                            break;
                        case 'b':
                            yOffset = -image1.Height;
                            break;
                    }

                    ibitmap(new Point(Convert.ToInt32(P.X + (xOffset * S)), Convert.ToInt32(P.Y + (yOffset * S))), image1);


            }
            catch (Exception ex)
            {
                report += "Symbol file not found for element " + el.ToString() + "\\n";
            }
        }

    }
}
