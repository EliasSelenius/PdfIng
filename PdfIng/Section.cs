using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;

namespace PdfIng {
    public abstract class Section : IHasGraphics {

        private IHasGraphics parent;
        public XGraphics Xg => parent.Xg;

        internal void Init(IHasGraphics p) {
            parent = p;
        }

        public void Render() {
            Header();
            Body();
            Footer();
        }

        protected virtual void Header() { }
        protected virtual void Body() { }
        protected virtual void Footer() { }


        protected class Cursor {
            public double x, y;
        }


        protected void Text(string text) {

        }

        protected void SubSection(Section section) {
            section.Init(this);
            section.Render();
        }
    }
}
