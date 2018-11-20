using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;

namespace PdfIng {
    public interface IHasGraphics {
        XGraphics Xg { get; }
    }
}
