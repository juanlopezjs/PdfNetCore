using System;

namespace MergePDF
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] filesNames = {
                    "/root1/file.pdf",
                    "/root2/file.pdf",
                };

                string outFile = "/finalRoot/file.pdf";

                var pdf = new PDF();
                pdf.MergePDF(filesNames, outFile);

                Console.WriteLine("successful merger");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
    }
}
