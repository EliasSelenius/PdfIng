using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfIng;

namespace PdfIngDemo {
    class Program {
        static void Main(string[] args) {
            var doc = new Document(new MySec());
            doc.Render();
            doc.SaveAs("TestOne.pdf");
            doc.Open();
        }
    }

    class MySec : Section {


        protected override void Header() {
            FontSize = 32;
            Margin = .05;
            WriteLine("Title");
        }


        protected override void Body() {

            FontSize = 15;
            Margin = .1;

            WriteLine("Hello World this is a test with lots of testing text haha" +
                 " sounds like texting text, haha its not realy funny i justy need something to talk about");

            WriteLine();

            WriteLine("This is a new paragraph");
            LeftMargin = .4;
            WriteLine("CENTERalMOST");

            LeftMargin = .4;

            TopMargin = BottomMargin = 0;

            for (int i = 0; i < 80; i++) {
                HorizontalMargin = i / (80f * 3);
                WriteLine("Test");
            }
        }

        protected override void Footer() {
            Margin = .1;
            WriteLine("Footer");
        }
    }
}
