using AutoMapper;
using MediatR;
using UserCRUD.Application.Contracts.Persistence.Identity;
using UserCRUD.Application.Features.File.Command.Delete;
using UserCRUD.Application.Features.File.Query.Get;
using UserCRUD.Application.Features.User.Query.Get;
using UserCRUD.Application.Models.DTOs.User;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Features.User.Command.Delete
{
    public class User_Delete_CommandHandler : IRequestHandler<User_Delete_Command,User_Delete_Response_Dto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public User_Delete_CommandHandler(IUserRepository userRepository, IMapper mapper, IMediator mediator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<User_Delete_Response_Dto> Handle(User_Delete_Command request, CancellationToken cancellationToken)
        {
            var model = await _userRepository.GetByPropertyAsync(new User_Get_Query()
            {
                Id = request.Id,
            });
            if (model == null)
            {
                return new User_Delete_Response_Dto(ResponseCodeEnum.RecordNotFound);
            }

            var files = await _mediator.Send(new File_Get_Query()
            {
                UserId = request.Id
            });

            foreach (var file in files.Files)
            {
                await _mediator.Send(new File_Delete_Command()
                {
                    Id = file.Id,
                });
            }

            _userRepository.Delete(model);
            var check = await _userRepository.CommitAsync();
            if (check == 0)
            {
                return new User_Delete_Response_Dto(ResponseCodeEnum.DeleteFailed);
            }

            return new User_Delete_Response_Dto(ResponseCodeEnum.Ok);
        }
    }
}
