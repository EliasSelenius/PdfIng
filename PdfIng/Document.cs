using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using PdfIng.Rendering;

namespace PdfIng {
    public class Document {

        private RenderObjectList renderObjects = new RenderObjectList();

        private PdfDocument pdfDocument;
        public int PageCount => pdfDocument.PageCount;

        public PdfPage CurrentPage;
        public double PageWidth => CurrentPage.Width;
        public double PageHeight => CurrentPage.Height;
        public int PageIndex = 0;
        public int PageNumber => PageIndex + 1;

        public XGraphics Xg { private set; get; }

        public string FileName { private set; get; }

        public Document(params RenderObject[] rs) {
            pdfDocument = new PdfDocument();
            NextPage();

            cursor = new Cursor(this);

            renderObjects.AddRange(rs);
        }

        public void NextPage() {
            PageIndex++;
            CurrentPage = pdfDocument.AddPage();
            Xg = XGraphics.FromPdfPage(CurrentPage);
        }

        public Cursor cursor;
        public class Cursor {
            private Document doc;
            public Cursor(Document d) {
                doc = d;
                ResetPos();
            }
            public double x;
            public double y;
            public void ResetPos() {
                //y = sec.tm;
            }
            public void MoveTo(double newX, double newY) {
                x = newX; y = newY;
                ValidatePos();
            }
            public void Move(double xdelta, double ydelta) {
                MoveTo(x + xdelta, y + ydelta);
            }
            public void MoveVertical(double ydelta) {
                Move(0, ydelta);
            }
            public void MoveToNextPage() {
                doc.NextPage();
                ResetPos();
            }
            private void ValidatePos() {
                if (SholdPageBreak(y)) {
                    MoveToNextPage();
                }
            }
            public bool SholdPageBreak(double vPos) {
                return vPos > doc.PageHeight;
            }
            public void PrepareForDrawing() {
                //x = sec.lm;
            }
        }

        public void Render() {
            renderObjects.RenderAll(this);
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
