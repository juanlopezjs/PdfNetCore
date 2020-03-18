using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace MergePDF
{
    public class PDF
    {
        public void MergePDF(string[] fileNames, string outFile)
        {
            var pdfWriter = new PdfWriter(outFile);
            var finalPdfDocument = new PdfDocument(pdfWriter);

            finalPdfDocument.InitializeOutlines();

            foreach (string fileName in fileNames)
            {
                var pdfDocument = new PdfDocument(new PdfReader(fileName));
                pdfDocument.CopyPagesTo(1, pdfDocument.GetNumberOfPages(), finalPdfDocument);
                pdfDocument.Close();
            }

            finalPdfDocument.Close();
            
        }

        public void AddImageToPDF(string pdfFileName, string imageFileName, string outFile)
        {
            // Modify PDF located at "pdfFileName" and save to "outFile"
            var pdfDocument = new PdfDocument(new PdfReader(pdfFileName), new PdfWriter(outFile));
            // Document to add layout elements: paragraphs, images etc
            var document = new Document(pdfDocument);

            // Load image from disk
            var imageData = ImageDataFactory.Create(imageFileName);
            // Create layout image object and provide parameters. Page number = 1
            var image = new Image(imageData)
                .ScaleAbsolute(100, 100)
                .SetFixedPosition(pdfDocument.GetNumberOfPages(), 25, 25);
            // This adds the image to the page
            document.Add(image);

            // Don't forget to close the document.
            // When you use Document, you should close it rather than PdfDocument instance
            document.Close();
        }
    }
}
