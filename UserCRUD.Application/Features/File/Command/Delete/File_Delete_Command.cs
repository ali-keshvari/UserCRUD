using MediatR;
using UserCRUD.Application.Models.DTOs.File;

namespace UserCRUD.Application.Features.File.Command.Delete
{
    public class File_Delete_Command : IRequest<File_Delete_Response_Dto>
    {
        public Guid Id { get; set; }
    }
}
