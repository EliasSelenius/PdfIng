using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;

namespace PdfIng.Rendering {
    public class TextArea : RenderObject {

        public string text;

        public TextArea(string t) {
            text = t;
        }

        protected override void Draw() {
          
        }
    }
}
