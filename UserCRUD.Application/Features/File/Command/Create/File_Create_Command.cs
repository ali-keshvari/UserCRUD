using MediatR;
using Microsoft.AspNetCore.Http;
using UserCRUD.Application.Models.DTOs.File;

namespace UserCRUD.Application.Features.File.Command.Create
{
    public class File_Create_Command : IRequest<File_Create_Response_Dto>
    {
        public IEnumerable<IFormFile> MultipleFiles { get; set; }
        public Guid UserId{ get; set; }
    }
}
