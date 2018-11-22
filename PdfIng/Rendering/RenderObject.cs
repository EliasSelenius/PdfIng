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

        public double x, y, Width, Height;

        private void Init() {
            Width = doc.PageWidth;
        }

        public void Bind(Document d) {
            doc = d;
            Init();
        }

        public void Render() {
            Draw();
        }

        protected abstract void Draw();
    }
}
