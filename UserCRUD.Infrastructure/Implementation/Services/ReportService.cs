using ClosedXML.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using UserCRUD.Application.Contracts.Services;
using UserCRUD.Application.Models.DTOs.User;

namespace UserCRUD.Infrastructure.Implementation.Services
{
    public class ReportService : IReportService
    {
        public async Task<string> ExcelReport(List<User_List_Item_Dto> users)
        {
			var fileDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "report", "excel");
			if (!Directory.Exists(fileDirectoryPath)) Directory.CreateDirectory(fileDirectoryPath);

			var fileName = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + "users.xlsx";
            var filePath = Path.Combine(fileDirectoryPath, fileName);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                worksheet.RightToLeft = true;

                worksheet.Cell(1, 1).Value = "نام";
                worksheet.Cell(1, 2).Value = "نام خانوادگی";
                worksheet.Cell(1, 3).Value = "کدملی";
                worksheet.Cell(1, 4).Value = "کدپرسنلی";

                for (int i = 2; i <= users.Count + 1; i++)
                {
                    var user = users[i - 2];
                    worksheet.Cell(i, 1).Value = user.FirstName;
                    worksheet.Cell(i, 2).Value = user.LastName;
                    worksheet.Cell(i, 3).Value = user.NationalCode;
                    worksheet.Cell(i, 4).Value = user.PersonalCode;
                }

                workbook.SaveAs(filePath);
            }


            return filePath;
        }

        public async Task<string> PdfReport(List<User_List_Item_Dto> users)
        {
            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "report", "pdf");
            if (!Directory.Exists(reportPath))
            {
                Directory.CreateDirectory(reportPath);
            }

            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                BaseFont baseFont = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\Arial.ttf", BaseFont.IDENTITY_H, true);
                Font font = new Font(baseFont, 12);
                Document myDocument = new Document();
                PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);

                myDocument.Open();

                PdfPTable table = new PdfPTable(4);
                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table.WidthPercentage = 100;

                PdfPCell header1 = new PdfPCell(new Phrase("نام",font));
                header1.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(header1);

                PdfPCell header2 = new PdfPCell(new Phrase("نام خانوادگی", font));
                header2.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(header2);

                PdfPCell header3 = new PdfPCell(new Phrase("کد ملی", font));
                header3.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(header3);

                PdfPCell header4 = new PdfPCell(new Phrase("کد پرسنلی", font));
                header4.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(header4);

                foreach (User_List_Item_Dto user in users)
                {
                    PdfPCell firstNameCell = new PdfPCell(new Phrase(user.FirstName, font));
                    firstNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(firstNameCell);

                    PdfPCell lastNameCell = new PdfPCell(new Phrase(user.LastName, font));
                    lastNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(lastNameCell);

                    PdfPCell nationalCodeCell = new PdfPCell(new Phrase(user.NationalCode, font));
                    nationalCodeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(nationalCodeCell);

                    PdfPCell PersonalCodeCell = new PdfPCell(new Phrase(user.PersonalCode, font));
                    PersonalCodeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(PersonalCodeCell);

                }

                myDocument.Add(table);
                myDocument.Close();

                byte[] content = myMemoryStream.ToArray();

                string filename = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + "users.pdf";
                string filePath = Path.Combine(reportPath, filename);

                try
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        fs.Write(content, 0, content.Length);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return filePath;
            }
        }
    }
}
