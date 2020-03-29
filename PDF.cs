using System;
using iText.Html2pdf;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PDF
{
    public class PDF
    {
        
        public string[] PdfFileNames { set; private get; }
        public string PdfFileName { set; private get; }
        public string WatermarkText { set; private get; }
        public string OutFile { set; private get; }
        public string ImageFileName { set; private get; }
        public float ImageWidth { set; private get; } = 100;
        public float ImageHeight { set; private get; } = 100;
        public int AlignX { set; private get; } = 190;
        public int AlignY { set; private get; } = 100;
        public int FontSize { set; private get; } = 90;
        public float RadAngle { set; private get; } = 1;
        public float Opacity { set; private get; } = 0.1f;
        public string Html { set; private get; }


        public void MergePDF()
        {
            var pdfWriter = new PdfWriter(OutFile);
            var finalPdfDocument = new PdfDocument(pdfWriter);

            finalPdfDocument.InitializeOutlines();

            foreach (string fileName in PdfFileNames)
            {
                var pdfDocument = new PdfDocument(new PdfReader(fileName));
                pdfDocument.CopyPagesTo(1, pdfDocument.GetNumberOfPages(), finalPdfDocument);
                pdfDocument.Close();
            }

            finalPdfDocument.Close();
            
        }

        public void AddImageToPDF()
        {
            var pdfDocument = new PdfDocument(new PdfReader(PdfFileName), new PdfWriter(OutFile));
            var document = new Document(pdfDocument);

            var imageData = ImageDataFactory.Create(ImageFileName);
            var image = new Image(imageData)
                .ScaleAbsolute(100, 100)
                .SetFixedPosition(pdfDocument.GetNumberOfPages(), 25, 25);

            document.Add(image);
            document.Close();
        }

        public void CreateWatermark()
        {
            var tranState = new PdfExtGState();
            tranState.SetFillOpacity(Opacity);

            var reader = new PdfReader(PdfFileName);
            var writer = new PdfWriter(OutFile);
            var pdf = new PdfDocument(reader, writer);

            var document = new Document(pdf);

            
            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {

                var page = pdf.GetPage(i);
                page.SetIgnorePageRotationForContent(false);
                var canvas = new PdfCanvas(pdf.GetPage(i));
                canvas.SaveState();
                canvas.SetExtGState(tranState);

                if (ImageFileName != null)
                {
                    var img = ImageDataFactory.Create(ImageFileName);
                    img.SetWidth(ImageWidth);
                    img.SetHeight(ImageHeight);
                    canvas.AddImage(img, AlignX, AlignY, false);
                }

                if (WatermarkText != null)
                {
                    var verticalWatermark = new Paragraph(WatermarkText).SetFontSize(FontSize);
                    document.ShowTextAligned(verticalWatermark, AlignX, AlignY, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, RadAngle);
                }
                    

                canvas.RestoreState();
                
            }
            WatermarkText = null;
            ImageFileName = null;
            pdf.Close();
            document.Close();
        }

        public void CreateWatermarkLeft()
        {
            Opacity = 1f;
            AlignX = 36;
            AlignY = 72;
            FontSize = 12;
            RadAngle = (float)Math.PI / 2;
            CreateWatermark();
        }

        public void ConvertHtmlToPdf()
        {
            HtmlConverter.ConvertToPdf( Html, new PdfWriter(OutFile));
        }
    }
}
