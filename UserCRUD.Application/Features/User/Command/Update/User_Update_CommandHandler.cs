using AutoMapper;
using MediatR;
using UserCRUD.Application.Contracts.Persistence.Identity;
using UserCRUD.Application.Contracts.Services;
using UserCRUD.Application.Features.File.Command.Create;
using UserCRUD.Application.Features.User.Query.Get;
using UserCRUD.Application.Models.DTOs.User;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Features.User.Command.Update
{
    public class User_Update_CommandHandler : IRequestHandler<User_Update_Command, User_Update_Responce_Dto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidFilesService _validFilesService;
        private readonly IVerifyNationalCodeService _verifyNationalCodeService;
        private readonly IMediator _mediator;

        public User_Update_CommandHandler(IUserRepository userRepository, IMapper mapper, IValidFilesService validFilesService, IVerifyNationalCodeService verifyNationalCodeService, IMediator mediator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validFilesService = validFilesService;
            _verifyNationalCodeService = verifyNationalCodeService;
            _mediator = mediator;
        }
        public async Task<User_Update_Responce_Dto> Handle(User_Update_Command request, CancellationToken cancellationToken)
        {
            var model = await _userRepository.GetByPropertyAsync(new User_Get_Query
            {
                Id = request.Id,
            });

            if (model == null)
            {
                return new User_Update_Responce_Dto(ResponseCodeEnum.RecordNotFound);
            }

            //var user = await _userRepository.GetByPropertyAsync(new User_Get_Query()
            //{
            //    PersonalCode = request.PersonalCode,
            //    NationalCode = request.NationalCode,
            //    DeniedId = request.Id
            //});

            var user = (await _userRepository.GetAsync(
                predicate: (u => !u.IsDeleted && !Equals(u.Id,request.Id) && (u.PersonalCode == request.PersonalCode || u.NationalCode == request.NationalCode))
                )).Items.FirstOrDefault();

            if (user != null)
            {
                if (user.NationalCode == request.NationalCode && user.PersonalCode == request.PersonalCode)
                {
                    return new User_Update_Responce_Dto(ResponseCodeEnum.DuplicatedUserWithNationalCodeAndPersonalCode);
                }
                if (user.NationalCode == request.NationalCode)
                {
                    return new User_Update_Responce_Dto(ResponseCodeEnum.DuplicatedUserWithNationalCode);
                }
                if (user.PersonalCode == request.PersonalCode)
                {
                    return new User_Update_Responce_Dto(ResponseCodeEnum.DuplicatedUserWithPersonalCode);
                }
            }

            if (request.MultipleFiles != null)
            {
                var validFiles = await _validFilesService.IsValid(request.MultipleFiles);
                if (!validFiles)
                {
                    return new User_Update_Responce_Dto(ResponseCodeEnum.UnValidFiles);
                }
            }
            

            var verifyUser = await _verifyNationalCodeService.Verify(request.NationalCode);
            if (!verifyUser)
            {
                return new User_Update_Responce_Dto(ResponseCodeEnum.UnValidNationalCode);
            }

            _mapper.Map(request, model);
            _userRepository.Update(model);

            var check = await _userRepository.CommitAsync();
            if (check == 0)
            {
                return new User_Update_Responce_Dto(ResponseCodeEnum.UpdateFailed);
            }

            if (request.MultipleFiles != null)
            {
                var res = await _mediator.Send(new File_Create_Command()
                {
                    MultipleFiles = request.MultipleFiles,
                    UserId = request.Id,
                });

                if (res.Ok)
                {
                    return new User_Update_Responce_Dto(ResponseCodeEnum.Ok);
                }
                return new User_Update_Responce_Dto(ResponseCodeEnum.AllFileNotUploaded);
            }

            return new User_Update_Responce_Dto(ResponseCodeEnum.Ok);
        }
    }
}
