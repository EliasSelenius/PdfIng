using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PdfIng.Rendering {
    public abstract class RenderObject {
        protected Section sec;

        public double x, y, Width, Height;

        private void Init() {
            Width = sec.DrawWidth;
        }

        public void Bind(Section s) {
            sec = s;
            Init();
        }

        public void Render() {
            Draw();
        }

        protected abstract void Draw();
    }
}
