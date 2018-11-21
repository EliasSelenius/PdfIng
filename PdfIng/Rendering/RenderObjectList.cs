﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfIng.Rendering {
    public class RenderObjectList : List<RenderObject> {
        public void RenderAll(Section sec) {
            ForEach(x => x.Bind(sec));
            ForEach(x => x.Render());
        }

        public double Height {
            get {
                double res = 0;
                ForEach(x => res += x.Height);
                return res;
            }
        }
    }
}
