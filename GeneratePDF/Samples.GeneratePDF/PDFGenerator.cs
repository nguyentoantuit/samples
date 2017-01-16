using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.IO;

namespace Samples.GeneratePDF
{
    public class PDFGenerator
    {
        private const string destinationPath = "D:\\Test.pdf";
        public static void CreatePdfFile()
        {
            byte[] fileContent = null;
            using (MemoryStream ms = new MemoryStream())
            {
                //Initialize PDF writer
                PdfWriter writer = new PdfWriter(ms);
                //Initialize PDF document
                PdfDocument pdf = new PdfDocument(writer);
                // Initialize document
                Document document = new Document(pdf, PageSize.A4);
                //Margin Left 0.5inch, Right 0.5inch, Top 0.5inch, Bottom 1inch
                document.SetMargins(36, 36, 72, 36);

                // Setup fonts
                PdfFont boldDefaultFont = PdfFontFactory.CreateRegisteredFont(FontConstants.HELVETICA_BOLD);
                PdfFont defaultFont = PdfFontFactory.CreateRegisteredFont(FontConstants.HELVETICA);
                var headerStyle = CreateStyle(boldDefaultFont, 18f);
                var tableHeaderStyle = CreateStyle(boldDefaultFont, 16f);
                var boldStyle = CreateStyle(boldDefaultFont, 11f);
                var normalStyle = CreateStyle(defaultFont, 11f);

                GenerateHeader(document, headerStyle);

                //Close document
                document.Close();
                fileContent = ms.ToArray();
            }

            SaveStreamToDocument(destinationPath, fileContent);

        }

        private static void GenerateHeader(Document document, Style titleStyle)
        {
            Table table = new Table(1);
            table.SetWidth(540f);
            table.SetBorder(Border.NO_BORDER);
            table.SetHorizontalAlignment(HorizontalAlignment.LEFT);
            table.SetMarginTop(10f);
            table.SetMarginBottom(25f);
            //Title of SPWeb
            string headerTitle = string.Format("Test Header Title {0}", DateTime.Today);
            //Heading
            Cell cellTitle = new Cell().Add(new Paragraph(headerTitle).AddStyle(titleStyle));
            cellTitle.SetBorder(Border.NO_BORDER);
            cellTitle.SetHorizontalAlignment(HorizontalAlignment.LEFT);
            table.AddCell(cellTitle);

            document.Add(table);
        }

        private static Style CreateStyle(PdfFont font, float size)
        {
            Style style = new Style();
            style.SetFont(font);
            style.SetFontSize(size);
            return style;
        }

        private static void SaveStreamToDocument(string destinationPath, byte[] ms)
        {
            File.WriteAllBytes(destinationPath, ms);
        }

    }
}
