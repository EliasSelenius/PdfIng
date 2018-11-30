using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfIng.Rendering {
    public class RenderObjectList : List<RenderObject> {
        public void RenderAll(Document doc) {
            ForEach(x => x.Bind(doc));

            ForEach(x => { x.Render(); doc.cursor.MoveVertical(x.Height); });
        }

        public void Add(RenderObject r, Document document) {
            r.Bind(document);
            base.Add(r);
        }

        public double Height => this.Max(x => x.Y + x.Height);
    }
}
