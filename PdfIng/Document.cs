using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PdfIng {
    public class Document : IHasGraphics {

        public Section RootSection;

        private PdfDocument pdfDocument;

        public PdfPage CurrentPage; 

        private XGraphics g;
        public XGraphics Xg => g;

        public string FileName { private set; get; }

        public Document(Section section) {
            pdfDocument = new PdfDocument();
            NextPage();

            RootSection = section;
            RootSection.Init(this, this);
        }

        public void NextPage() {
            CurrentPage = pdfDocument.AddPage();
            g = XGraphics.FromPdfPage(CurrentPage);
        }

        public void Render() {
            RootSection.Render();
        }

        public void SaveAs(string fileName) {
            FileName = fileName;
            pdfDocument.Save(fileName);
        }

        public void Open() {
            System.Diagnostics.Process.Start(FileName);
        }

    }
}
