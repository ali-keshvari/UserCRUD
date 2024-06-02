using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserCRUD.Application.Common.Constants;
using UserCRUD.Application.Contracts.Services;
using UserCRUD.Application.Features.File.Command.Delete;
using UserCRUD.Application.Features.File.Query.Get;
using UserCRUD.Application.Features.User.Command.Create;
using UserCRUD.Application.Features.User.Command.Delete;
using UserCRUD.Application.Features.User.Command.Update;
using UserCRUD.Application.Features.User.Query.Get;
using UserCRUD.Domain.Enum;
using UserCRUD.Ui.Models;

namespace UserCRUD.Ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IReportService _reportService;

        public HomeController(IMapper mapper, IMediator mediator, IReportService reportService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int page = 1)
        {
            var model = await _mediator.Send(new User_Get_Query
            {
                PageNum = page,
                OrderByList = new() { nameof(Domain.Entities.Identity.User.CreatedAt) },
                OrderType = OrderTypeEnum.Descending
            });

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Add(User_Create_Command model)
        {
            var res = await _mediator.Send(model);

            return Json(res);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = (await _mediator.Send(new User_Get_Query()
            {
                Id = id
            })).Users.FirstOrDefault();

            var model = _mapper.Map<User_Update_Command>(user);

            return PartialView(model);
        }

        [HttpPost]
        public async Task<JsonResult> Edit(User_Update_Command model)
        {
            var res = await _mediator.Send(model);
            return Json(res);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var res = await _mediator.Send(new User_Delete_Command()
            {
                Id = id
            });
            return Json(res);
        }
        
    }
}
