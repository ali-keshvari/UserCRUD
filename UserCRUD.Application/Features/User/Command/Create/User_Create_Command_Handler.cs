using AutoMapper;
using MediatR;
using UserCRUD.Application.Contracts.Persistence.Identity;
using UserCRUD.Application.Contracts.Services;
using UserCRUD.Application.Features.File.Command.Create;
using UserCRUD.Application.Features.User.Query.Get;
using UserCRUD.Application.Models.DTOs.User;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Features.User.Command.Create
{
    public class User_Create_Command_Handler : IRequestHandler<User_Create_Command,User_Create_Response_Dto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerifyNationalCodeService _verifyNationalCodeService;
        private readonly IValidFilesService _validFilesService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public User_Create_Command_Handler(IMapper mapper, IUserRepository userRepository, IVerifyNationalCodeService verifyNationalCodeService, IValidFilesService validFilesService, IMediator mediator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _verifyNationalCodeService = verifyNationalCodeService;
            _validFilesService = validFilesService;
            _mediator = mediator;
        }

        public async Task<User_Create_Response_Dto> Handle(User_Create_Command request, CancellationToken cancellationToken)
        {
            var model = (await _mediator.Send(new User_Get_Query()
            {
                PersonalCode = request.PersonalCode,
                NationalCode = request.NationalCode,
            })).Users.FirstOrDefault();

            if (model != null)
            {
                if (model.NationalCode == request.NationalCode && model.PersonalCode == request.PersonalCode)
                {
                    return new User_Create_Response_Dto(ResponseCodeEnum.DuplicatedUserWithNationalCodeAndPersonalCode);
                }
                if (model.NationalCode == request.NationalCode)
                {
                    return new User_Create_Response_Dto(ResponseCodeEnum.DuplicatedUserWithNationalCode);
                }
                if (model.PersonalCode == request.PersonalCode)
                {
                    return new User_Create_Response_Dto(ResponseCodeEnum.DuplicatedUserWithPersonalCode);
                }

            }

            var validFiles = await _validFilesService.IsValid(request.MultipleFiles);
            if (!validFiles)
            {
                return new User_Create_Response_Dto(ResponseCodeEnum.UnValidFiles);
            }

            var verifyUser = await _verifyNationalCodeService.Verify(request.NationalCode);
            if (!verifyUser)
            {
                return new User_Create_Response_Dto(ResponseCodeEnum.UnValidNationalCode);
            }

            var createModel = _mapper.Map<Domain.Entities.Identity.User>(request);
            _userRepository.Add(createModel);

            var check = await _userRepository.CommitAsync();
            if (check == 0)
            {
                return new User_Create_Response_Dto(ResponseCodeEnum.CreateFailed);
            }

            var res = await _mediator.Send(new File_Create_Command()
            {
                MultipleFiles = request.MultipleFiles,
                UserId = createModel.Id
            });

            if (res.Ok)
            {
                return new User_Create_Response_Dto(ResponseCodeEnum.Ok);
            }

            return new User_Create_Response_Dto(ResponseCodeEnum.AllFileNotUploaded);
        }
    }
}
