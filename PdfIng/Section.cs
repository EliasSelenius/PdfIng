using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;

namespace PdfIng {

    public abstract class Section : IHasGraphics {

        protected Document Doc;
        protected IHasGraphics parent;
        public XGraphics Xg => Doc.Xg;

        protected double PageWidth => Doc.CurrentPage.Width;
        protected double PageHeight => Doc.CurrentPage.Height;

        internal void Init(Document d, IHasGraphics p) {
            Doc = d; parent = p;

            Default();
            cursor = new Cursor(this);
        }

        protected void Default() {
            Margin = .1;

            FontSize = 11;
        }

        public void Render() {
            Header();
            Body();
            Footer();
        }

        protected virtual void Header() { }
        protected virtual void Body() { }
        protected virtual void Footer() { }

        protected Cursor cursor;
        protected class Cursor {
            private Section sec;
            public Cursor(Section s) {
                sec = s;
                ResetPos();
            }
            public double x;
            public double y;
            public void ResetPos() {
                y = sec.tm + sec.Font.Size;
            }
            public void MoveToNextLine() {
                y += sec.Font.Size;
                ValidatePos();
            }
            public void MoveToNextPage() {
                sec.Doc.NextPage();
                ResetPos();
            }
            private void ValidatePos() {
                if (SholdPageBreak(y)) {
                    MoveToNextPage();
                }
            }
            public bool SholdPageBreak(double vPos) {
                return vPos > sec.PageHeight - sec.bm;
            }
        }


        protected double FontSize;
        protected XFont Font => new XFont("Verdana", FontSize, XFontStyle.Regular);


        private double lm, rm, tm, bm;

        protected double LeftMargin {
            set {
                lm = PageWidth * value;
            }
        }
        protected double RightMargin {
            set {
                rm = PageWidth * value;
            }
        }
        protected double TopMargin {
            set {
                tm = PageWidth * value;
            }
        }
        protected double BottomMargin {
            set {
                bm = PageWidth * value;
            }
        }
        protected double Margin {
            set {
                LeftMargin = RightMargin = TopMargin = BottomMargin = value;
            }
        } 
        protected double HorizontalMargin {
            set {
                LeftMargin = RightMargin = value;
            }
        }
        protected double VerticalMargin {
            set {
                TopMargin = BottomMargin = value;
            }
        }

        protected double DrawWidth => PageWidth - lm - rm;

        private void PrepareCursorForDrawing() {
            cursor.x = lm;
        }

        protected void WriteLine(string text = "") {

            PrepareCursorForDrawing();

            string[] words = text.Split(' ');
            string line = "";
            int index = 0;

            void Draw() {
                Xg.DrawString(line, Font, XBrushes.Black, cursor.x, cursor.y);
                line = "";
                cursor.MoveToNextLine();
            }

            while (index < words.Length) {
                if (Xg.MeasureString(line, Font).Width + Xg.MeasureString(words[index], Font).Width < DrawWidth) {
                    line += words[index] + " ";
                    index++;
                } else {
                    Draw();
                }
            }
            Draw();
        }
        protected void Image(string path) {
            PrepareCursorForDrawing();

            XImage image = XImage.FromFile(path);

            Console.WriteLine(image.PixelWidth);
            Console.WriteLine(image.PixelHeight);
            double aspect = image.PixelWidth / (double)image.PixelHeight;
            Console.WriteLine(aspect);

            double h = DrawWidth / aspect;
            Console.WriteLine(DrawWidth);
            Console.WriteLine(h);
            Console.Read();
            if (cursor.SholdPageBreak(cursor.y + h)) {
                cursor.MoveToNextPage();
            }

            Xg.DrawImage(image, cursor.x, cursor.y, DrawWidth, h);
            cursor.y += h;
            cursor.MoveToNextLine();
        }
        protected void SubSection(Section section) {
            section.Init(Doc, this);
            section.Render();
        }
    }
}
