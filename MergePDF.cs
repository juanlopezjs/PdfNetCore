using iText.Kernel.Pdf;

namespace MergePDF
{
    public class PDF
    {
        public void MergePDF(string[] fileNames, string outFile)
        {
            var writer = new PdfWriter(outFile);
            var pdfDoc = new PdfDocument(writer);

            pdfDoc.InitializeOutlines();

            foreach (string fileName in fileNames)
            {
                var addedDoc = new PdfDocument(new PdfReader(fileName));
                addedDoc.CopyPagesTo(1, addedDoc.GetNumberOfPages(), pdfDoc);
                addedDoc.Close();
            }

            pdfDoc.Close();
        }
    }
}
