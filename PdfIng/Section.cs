using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;

using PdfIng.Rendering;

namespace PdfIng {

    public abstract class Section : RenderObject {

        protected RenderObjectList renderObjects;

        protected double PageWidth => doc.PageWidth;
        protected double PageHeight => doc.PageHeight;

        protected override void Init() { 
            Default();
        }

        protected void Default() {
            Margin = .1;

            FontSize = 11;
        }

        protected override void Draw() {


            renderObjects = new RenderObjectList();

            Header();


            Body();


            Footer();

            renderObjects.RenderAll(doc);
        }

        protected virtual void Header() { }
        protected virtual void Body() { }
        protected virtual void Footer() { }


        public override double Height {
            get => renderObjects.Height;
            set => throw new NotImplementedException();
        }

        protected double FontSize;
        public XFont Font => new XFont("Verdana", FontSize, XFontStyle.Regular);


        private double lm, rm, tm, bm;

        protected double LeftMargin {
            set {
                lm = PageWidth * value;
            }
            get {
                return lm;
            }
        }
        protected double RightMargin {
            set {
                rm = PageWidth * value;
            }
            get {
                return rm;
            }
        }
        protected double TopMargin {
            set {
                tm = PageWidth * value;
            }
            get {
                return tm;
            }
        }
        protected double BottomMargin {
            set {
                bm = PageWidth * value;
            }
            get {
                return bm;
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

        private double verticalPos;
        
        protected TextArea WriteLine(string text = "") {

            TextArea textArea = new TextArea(text) {
                X = LeftMargin,
                Y = verticalPos,
                Width = DrawWidth,
                FontSize = FontSize
            };

            renderObjects.Add(textArea, doc);

            verticalPos += textArea.Height;
            return textArea;
        }
        
        /*
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
