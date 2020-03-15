using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MergePDF
{
    public class PDF
    {
        public void MergePDF(string[] fileNames, string outFile)
        {
            Document document = new Document();
            //create newFileStream object which will be disposed at the end
            FileStream newFileStream = new FileStream(outFile, FileMode.Create);
            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, newFileStream);

            // step 3: we open the document
            document.Open();

            foreach (string fileName in fileNames)
            {
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(fileName);
                reader.ConsolidateNamedDestinations();

                // step 4: we add content
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    writer.AddPage(writer.GetImportedPage(reader, i));
                }

                reader.Close();
            }

            // step 5: we close the document and writer
            writer.Close();
            document.Close();
        }
    }
}
