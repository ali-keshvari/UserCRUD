using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Application.Models.DTOs.File;
using UserCRUD.Domain.Entities.Common;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Features.File.Command.Create
{
	public class File_Create_CommandHandler : IRequestHandler<File_Create_Command, File_Create_Response_Dto>
	{
		private readonly IWebHostEnvironment _env;
		private readonly IFileRepository _fileRepository;
		private readonly IMapper _mapper;

		public File_Create_CommandHandler(IWebHostEnvironment env, IFileRepository fileRepository)
		{
			_env = env;
			_fileRepository = fileRepository;
		}

		public async Task<File_Create_Response_Dto> Handle(File_Create_Command request, CancellationToken cancellationToken)
		{
			var fileDirectoryPath = Path.Combine(_env.WebRootPath, "files");
			if (!Directory.Exists(fileDirectoryPath))
				Directory.CreateDirectory(fileDirectoryPath);

			bool res = true;

			foreach (var file in request.MultipleFiles)
			{
				var extension = Path.GetExtension(file.FileName);
				var fileName = Guid.NewGuid() + extension;

				var filePath = Path.Combine(fileDirectoryPath, fileName);

				try
				{
					await using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream, cancellationToken);
					}

					var createModel = new Upload_File()
					{
						Path = Path.Combine("files",fileName),
						FileName = file.FileName,
						UserId = request.UserId,
					};

					_fileRepository.Add(createModel);

					if (await _fileRepository.CommitAsync() < 1) res = false;
				}
				catch
				{
					if (System.IO.File.Exists(filePath))
						System.IO.File.Delete(filePath);

					res = false;
				}
			}

			return res ? 
				new File_Create_Response_Dto(ResponseCodeEnum.Ok) 
				: new File_Create_Response_Dto(ResponseCodeEnum.AllFileNotUploaded);
		}
	}
}
