using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;


namespace PdfIng.Rendering {
    public abstract class RenderObject {
        protected Document doc;
        protected XGraphics Xg => doc.Xg;
        protected Document.Cursor Cursor => doc.cursor;

        protected double? x, y, width;

        public abstract double Height { get; set; }

        public double X {
            get => (x == null) ? 0 : (double)x;
            set => x = value;
        }
        public double Y {
            get => (y == null) ? 0 : (double)y;
            set => y = value;
        }
        public double Width {
            get => (width == null)? 0 : (double)width;
            set => width = value;
        }


        private void _init() {
            if (width == null) {
                width = doc.PageWidth;
            }
            Init();
        }

        public void Bind(Document d) {
            doc = d;
            _init();
        }

        public void Render() {
            Draw();
        }

        protected virtual void Init() { }
        protected abstract void Draw();
    }
}
