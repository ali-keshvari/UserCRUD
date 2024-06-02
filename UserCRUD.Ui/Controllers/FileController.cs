using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserCRUD.Application.Features.File.Command.Delete;
using UserCRUD.Application.Features.File.Query.Get;

namespace UserCRUD.Ui.Controllers
{
    
    public class FileController : Controller
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var model = await _mediator.Send(new File_Get_Query()
            {
                UserId = id,
                Limit = Int32.MaxValue
            });

            return PartialView(model);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var res = await _mediator.Send(new File_Delete_Command()
            {
                Id = id
            });
            return Json(res);
        }

        
    }
}
