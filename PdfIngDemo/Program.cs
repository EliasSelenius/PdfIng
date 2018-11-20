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
            string fileName = "TestOne.pdf";
            doc.SaveAs(fileName);
            System.Diagnostics.Process.Start(fileName);
        }
    }

    class MySec : Section {
        protected override void Body() {
            WriteLine("Hello World this is a test with lots of testing text haha" +
                 " sounds like texting text, haha its not realy funny i justy need something to talk about");

            WriteLine();

            WriteLine("This is a new paragraph");

            HorizontalMargin = .12;

            string longString = "Hello ";
            for (int i = 0; i < 10; i++) {
                longString += longString;
            }

            WriteLine(longString);

        }
    }
}
