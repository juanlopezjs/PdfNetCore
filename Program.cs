using System;
using System.IO;

namespace PDF
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var path = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), "..//..//..//"));
                var pdf = new PDF();

                /*BEGIN MERGE*/
                pdf.PdfFileNames = new [] {
                    $"{path}/assets/sample1.pdf",
                    $"{path}/assets/sample2.pdf"
                };
                pdf.OutFile = $"{path }/result/file.pdf";
                pdf.MergePDF();
                /*END MERGE*/

                /*BEGIN WATERMARK*/

                /*IMAGE*/
                pdf.AlignX = 80;
                pdf.ImageWidth = 500;
                pdf.ImageHeight = 500;
                pdf.PdfFileName = $"{path}/assets/sample1.pdf";
                pdf.ImageFileName = $"{path}/assets/image.png";
                pdf.OutFile = $"{path }/result/file2.pdf";
                pdf.CreateWatermark();

                /*TEXT*/
                pdf.WatermarkText = "Marca De Agua";
                pdf.AlignX = 190;
                pdf.PdfFileName = $"{path}/assets/sample2.pdf";
                pdf.OutFile = $"{path }/result/file3.pdf";
                pdf.CreateWatermark();
                /*END WATERMARK*/

                /*BEGIN WATERMARK LEFT*/
                pdf.PdfFileName = $"{path}/assets/sample2.pdf";
                pdf.WatermarkText = "Prueba";
                pdf.OutFile = $"{path }/result/file4.pdf";
                pdf.CreateWatermarkLeft();
                /*END WATERMARK LEFT*/

                /*BEGIN SIGNATURE IMAGE*/
                pdf.ImageFileName = $"{path}/assets/signature.png";
                pdf.PdfFileName = $"{path}/assets/sample1.pdf";
                pdf.OutFile = $"{path}/result/file5.pdf";
                pdf.AddImageToPDF();
                /*END SIGNATURE IMAGE*/

                /*BEGIN HTML TO PDF*/
                pdf.Html = File.ReadAllText($"{path}/assets/HtmlFile.html");
                pdf.OutFile = $"{path }/result/file6.pdf";
                pdf.ConvertHtmlToPdf();
                /*END HTML TO PDF*/

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
