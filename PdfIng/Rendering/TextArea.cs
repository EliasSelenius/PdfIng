using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;

namespace PdfIng.Rendering {
    public class TextArea : RenderObject {

        public string text;
        public double FontSize = 11;

        private XFont Font => new XFont("Verdana", FontSize, XFontStyle.Regular);

        public override double Height {
            get {
                double span = Xg.MeasureString(text, Font).Width / Width;
                return Math.Ceiling(span) * FontSize;
            }
            set => throw new NotImplementedException();
        }

        public TextArea(string t) {
            text = t;
        }

        protected override void Draw() {
            Cursor.PrepareForDrawing();

            string[] words = text.Split(' ');
            string line = "";
            int index = 0;

            Cursor.MoveTo(X, Y);

            void _Draw() {
                Xg.DrawString(line, Font, XBrushes.Black, Cursor.x, Cursor.y + FontSize);
                line = "";
                doc.cursor.MoveVertical(FontSize);
            }

            while (index < words.Length) {
                if (Xg.MeasureString(line, Font).Width + Xg.MeasureString(words[index], Font).Width < Width) {
                    line += words[index] + " ";
                    index++;
                } else {
                    _Draw();
                }
            }
            _Draw();
        }
    }
}
