using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;

using PdfIng.Rendering;

namespace PdfIng {

    public abstract class Section {

        protected Document document;
        public XGraphics Xg => document.Xg;

        protected double PageWidth => document.PageWidth;
        protected double PageHeight => document.PageHeight;

        private RenderObjectList renderObjects;

        internal void Init(Document d) {
            document = d;

            Default();
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

        


        protected double FontSize;
        public XFont Font => new XFont("Verdana", FontSize, XFontStyle.Regular);


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

        public double DrawWidth => PageWidth - lm - rm;

        /*
        protected void WriteLine(string text = "") {

            cursor.PrepareForDrawing();

            string[] words = text.Split(' ');
            string line = "";
            int index = 0;

            void Draw() {
                Xg.DrawString(line, Font, XBrushes.Black, cursor.x, cursor.y + FontSize);
                line = "";
                cursor.MoveDown(FontSize);
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
            cursor.PrepareForDrawing();

            XImage image = XImage.FromFile(path);
            double aspect = image.PixelWidth / (double)image.PixelHeight;

            double h = DrawWidth / aspect;
            if (cursor.SholdPageBreak(cursor.y + h)) {
                cursor.MoveToNextPage();
            }

            Xg.DrawImage(image, cursor.x, cursor.y, DrawWidth, h);
            cursor.y += h;
            cursor.MoveDown(FontSize);
        }
        protected void SubSection(Section section) {
            section.Init(document);
            section.Render();
        }
        */
    }
}
