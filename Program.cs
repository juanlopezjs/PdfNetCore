using System;

namespace MergePDF
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filesNames = {
                "/root/file1.pdf",
                "/root/file2.pdf"
            };

            string outFile = "/finalRoot/file.pdf";

            var pdf = new PDF();
            pdf.MergePDF(filesNames, outFile);

            Console.WriteLine("Hello World!");
        }
    }
}
