using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfIng;
using PdfIng.Rendering;

using System.Xml.Linq;

namespace PdfIngDemo {
    class Program {
        static void Main(string[] args) {

            var doc = new Document(new TextArea("Hello World this is a test with lots of testing text haha" +
                 " sounds like texting text, haha its not realy funny i justy need something to talk about"), new TextArea("Hello World") { Y = 22, FontSize = 30 });
            doc.Append(new TextArea("Hello World this is a test with lots of testing text haha" +
                 " sounds like texting text, haha its not realy funny i justy need something to talk about") { X = 50, Y = 200, Width = 200 });

            doc.Append(new TextArea("ajdioawjdojaodkd  wapdkpwad kpwdk kpdw kfw kfw kapfk wpa fwpkepgj egio") { X = 350, Y = 200, Width = 200 });

            string longstring = "Hey ";
            for (int i = 0; i < 10; i++) {
                longstring += longstring;
            }

            doc.Append(new TextArea(longstring) {
                Y = 500
            });

            doc.Render();
            doc.SaveAs("TestOne.pdf");
            doc.Open();
        }
    }

    /*
    class MySec : Section {


        protected override void Header() {
            FontSize = 32;
            WriteLine("Title");
        }


        protected override void Body() {

            FontSize = 11;

            WriteLine("Hello World this is a test with lots of testing text haha" +
                 " sounds like texting text, haha its not realy funny i justy need something to talk about");

            WriteLine();

            WriteLine("This is a new paragraph");
            LeftMargin = .4;
            WriteLine("CENTERalMOST");

            LeftMargin = .4;

            TopMargin = BottomMargin = 0;

            RightMargin = 0;
            for (int i = 0; i < 80; i++) {
                LeftMargin = i / (90f);
                WriteLine("Test");
            }

            Margin = .4;
            BottomMargin = 0;
            RightMargin = 0;
            Image("img.png");
        }

        protected override void Footer() {
            Margin = .1;
            BottomMargin = 0;
            WriteLine("Footer");
        }
    }

    */
}
