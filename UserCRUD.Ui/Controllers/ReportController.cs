using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserCRUD.Application.Common.Constants;
using UserCRUD.Application.Contracts.Services;
using UserCRUD.Application.Features.User.Query.Get;

namespace UserCRUD.Ui.Controllers
{
    public class ReportController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IReportService _reportService;

        public ReportController(IMediator mediator, IReportService reportService)
        {
            _mediator = mediator;
            _reportService = reportService;
        }
        public async Task<IActionResult> GetExcel()
        {
            var users = await _mediator.Send(new User_Get_Query() { Limit = int.MaxValue });
            var filePath = await _reportService.ExcelReport(users.Users); // Assuming _reportService returns the file path

            if (System.IO.File.Exists(filePath))
            {
                var fileName = Path.GetFileName(filePath); // Get file name from path
                var fileContent = await System.IO.File.ReadAllBytesAsync(filePath); // Read file content as byte array

                return File(fileContent, MimeTypes.Xlsx, fileName);
            }
            else
            {
                // Handle error if file doesn't exist
                return NotFound(); // Or return an appropriate error message
            }
        }
        public async Task<IActionResult> GetPdf()
        {
            var users = await _mediator.Send(new User_Get_Query() { Limit = int.MaxValue });
            var filePath = await _reportService.PdfReport(users.Users); // Assuming _reportService returns the file path

            if (System.IO.File.Exists(filePath))
            {
                var fileName = Path.GetFileName(filePath); // Get file name from path
                var fileContent = System.IO.File.ReadAllBytes(filePath); // Read file content as byte array

                return File(fileContent, MimeTypes.Pdf, fileName);
            }
            else
            {
                // Handle error if file doesn't exist
                return NotFound(); // Or return an appropriate error message
            }
        }
    }
}
