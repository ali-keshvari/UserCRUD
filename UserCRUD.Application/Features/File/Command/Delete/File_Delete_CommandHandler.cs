using AutoMapper;
using MediatR;
using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Application.Features.File.Query.Get;
using UserCRUD.Application.Models.DTOs.File;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Features.File.Command.Delete
{
    public class File_Delete_CommandHandler : IRequestHandler<File_Delete_Command,File_Delete_Response_Dto>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public File_Delete_CommandHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        public async Task<File_Delete_Response_Dto> Handle(File_Delete_Command request, CancellationToken cancellationToken)
        {
            var model = await _fileRepository.GetByPropertyAsync(new File_Get_Query()
            {
                Id = request.Id,
            });
            if (model == null)
            {
                return new File_Delete_Response_Dto(ResponseCodeEnum.RecordNotFound);
            }

            _fileRepository.Delete(model);
            var check = await _fileRepository.CommitAsync();
            if (check == 0)
            {
                return new File_Delete_Response_Dto(ResponseCodeEnum.DeleteFailed);
            }

            return new File_Delete_Response_Dto(ResponseCodeEnum.Ok);
        }
    }
}
