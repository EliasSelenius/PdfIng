using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PdfIng {
    public class Document {

        public Section RootSection;

        private PdfDocument pdfDocument;
        public int PageCount => pdfDocument.PageCount;

        public PdfPage CurrentPage;
        public double PageWidth => CurrentPage.Width;
        public double PageHeight => CurrentPage.Height;
        public int PageIndex = 0;
        public int PageNumber => PageIndex + 1;

        public XGraphics Xg { private set; get; }

        public string FileName { private set; get; }

        public Document(Section section) {
            pdfDocument = new PdfDocument();
            NextPage();

            RootSection = section;
            RootSection.Init(this);
        }

        public void NextPage() {
            PageIndex++;
            CurrentPage = pdfDocument.AddPage();
            Xg = XGraphics.FromPdfPage(CurrentPage);
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
