using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;

namespace PdfIng {

    public enum TextAlign {
        Left,
        Center,
        Right
    }


    public abstract class Section : IHasGraphics {

        private Document Doc;
        private IHasGraphics parent;
        public XGraphics Xg => Doc.Xg;

        protected double PageWidth => Doc.CurrentPage.Width;
        protected double PageHeight => Doc.CurrentPage.Height;

        internal void Init(Document d, IHasGraphics p) {
            Doc = d; parent = p;

            Margin = .1;
            cursor = new Cursor(this);
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
            private void ValidatePos() {
                if (y > sec.PageHeight - sec.bm) {
                    sec.Doc.NextPage();
                    ResetPos();
                }
            }
        }


        protected XFont Font = new XFont("Verdana", 11, XFontStyle.Regular);


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

        protected double MaxTextLineWidth => Doc.CurrentPage.Width.Value - lm - rm;

        protected void WriteLine(string text = "") {

            string[] words = text.Split(' ');

            cursor.x = lm;

            string line = "";
            int index = 0;

            void Draw() {
                Xg.DrawString(line, Font, XBrushes.Black, cursor.x, cursor.y);
                line = "";
                cursor.MoveToNextLine();
            }

            while (index < words.Length) {
                if (Xg.MeasureString(line, Font).Width + Xg.MeasureString(words[index], Font).Width < MaxTextLineWidth) {
                    line += " " + words[index];
                    index++;
                } else {
                    Draw();
                }
            }
            Draw();
        }

        protected void SubSection(Section section) {
            section.Init(Doc, this);
            section.Render();
        }
    }
}
