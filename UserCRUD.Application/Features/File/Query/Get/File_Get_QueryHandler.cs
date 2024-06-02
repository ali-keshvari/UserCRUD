using AutoMapper;
using MediatR;
using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Application.Models.DTOs.File;

namespace UserCRUD.Application.Features.File.Query.Get
{
    public class File_Get_QueryHandler : IRequestHandler<File_Get_Query,File_List_Response_Dto>
    {
		private readonly IMapper _mapper;
		private readonly IFileRepository _fileRepository;

		public File_Get_QueryHandler(IMapper mapper, IFileRepository fileRepository)
		{
			_mapper = mapper;
			_fileRepository = fileRepository;
		}

		public async Task<File_List_Response_Dto> Handle(File_Get_Query request, CancellationToken cancellationToken)
        {
			var res = new File_List_Response_Dto();

			var files = await _fileRepository.Search<File_List_Dto, File_List_Item_Dto>(request);

			res.Files.AddRange(files.Rows);

			_mapper.Map(files, res);

			return res;
		}
    }
}
