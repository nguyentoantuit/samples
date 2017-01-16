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
    public class CommonStyle
    {
        public Style HeaderStyle { get; set; }
        public Style TableHeaderStyle { get; set; }
        public Style BoldStyle { get; set; }
        public Style NormalStyle { get; set; }

        public CommonStyle()
        {
            // Setup fonts
            PdfFont boldDefaultFont = PdfFontFactory.CreateRegisteredFont(FontConstants.HELVETICA_BOLD);
            PdfFont defaultFont = PdfFontFactory.CreateRegisteredFont(FontConstants.HELVETICA);
            HeaderStyle = CreateStyle(boldDefaultFont, 18f);
            TableHeaderStyle = CreateStyle(boldDefaultFont, 16f);
            BoldStyle = CreateStyle(boldDefaultFont, 11f);
            NormalStyle = CreateStyle(defaultFont, 11f);

        }

        private static Style CreateStyle(PdfFont font, float size)
        {
            Style style = new Style();
            style.SetFont(font);
            style.SetFontSize(size);
            return style;
        }
    }

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
                CommonStyle commonStyle = new CommonStyle();

                GenerateHeader(document, commonStyle);

                GeneratePersonalInfo(document, commonStyle);

                //Close document
                document.Close();
                fileContent = ms.ToArray();
            }

            SaveStreamToDocument(destinationPath, fileContent);

        }

        private static void GeneratePersonalInfo(Document document, CommonStyle commonStyle)
        {
            float[] widths = new float[] { 2.5f, 3.5f, 2.5f, 3.5f };
            Table table = CreatePdfTable(widths);

            //Heading
            Cell cellTitle = new Cell(1, widths.Length).Add(new Paragraph("Personal Information").AddStyle(commonStyle.TableHeaderStyle));
            cellTitle.SetBorder(Border.NO_BORDER);
            cellTitle.SetHorizontalAlignment(HorizontalAlignment.LEFT);
            table.AddCell(cellTitle);
            table.AddCell(CreateTableCellSeperator(widths.Length));

            table.AddCell(new Cell().Add(new Paragraph("FirstName").AddStyle(commonStyle.BoldStyle)).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("Toan").AddStyle(commonStyle.NormalStyle)).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("LastName").AddStyle(commonStyle.BoldStyle)).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("Nguyen").AddStyle(commonStyle.NormalStyle)).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("Email").AddStyle(commonStyle.BoldStyle)).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("nguyentoanuit@gmail.com").AddStyle(commonStyle.NormalStyle)).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("Phone Number").AddStyle(commonStyle.BoldStyle)).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("123 456 789").AddStyle(commonStyle.NormalStyle)).SetBorder(Border.NO_BORDER));

            table.AddCell(CreateTableCellSeperator(widths.Length));

            table.AddCell(new Cell().Add(new Paragraph("Gender").AddStyle(commonStyle.BoldStyle)).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("Male").AddStyle(commonStyle.NormalStyle)).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("Adress").AddStyle(commonStyle.BoldStyle)).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("Ho Chi Minh, Vietnam").AddStyle(commonStyle.NormalStyle)).SetBorder(Border.NO_BORDER));
            
            document.Add(table);
        }

        private static Table CreatePdfTable(float[] widths)
        {
            Table table = new Table(widths);
            table.SetBorder(Border.NO_BORDER);
            table.SetWidth(540f);

            table.SetHorizontalAlignment(HorizontalAlignment.LEFT);
            table.SetMarginTop(10f);
            table.SetMarginBottom(10f);
            return table;
        }

        private static Cell CreateTableCellSeperator(int colsSpan)
        {
            return new Cell(1, colsSpan).SetMarginBottom(10f).SetBorder(Border.NO_BORDER);
        }

        private static void GenerateHeader(Document document, CommonStyle commonStyle)
        {
            Table table = new Table(1);
            table.SetWidth(540f);
            table.SetBorder(Border.NO_BORDER);
            table.SetHorizontalAlignment(HorizontalAlignment.LEFT);
            table.SetMarginTop(10f);
            table.SetMarginBottom(25f);
            string headerTitle = string.Format("Test Header Title {0}", DateTime.Now);
            //Heading
            Cell cellTitle = new Cell().Add(new Paragraph(headerTitle).AddStyle(commonStyle.HeaderStyle));
            cellTitle.SetBorder(Border.NO_BORDER);
            cellTitle.SetHorizontalAlignment(HorizontalAlignment.LEFT);
            table.AddCell(cellTitle);

            document.Add(table);
        }

        private static void SaveStreamToDocument(string destinationPath, byte[] ms)
        {
            File.WriteAllBytes(destinationPath, ms);
        }
    }
}
