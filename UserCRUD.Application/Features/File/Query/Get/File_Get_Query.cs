using MediatR;
using UserCRUD.Application.Models.Common;
using UserCRUD.Application.Models.DTOs.File;

namespace UserCRUD.Application.Features.File.Query.Get
{
    public class File_Get_Query : Base_Paging_Dto<Guid>, IRequest<File_List_Response_Dto>
    {
        public Guid? UserId { get; set; }
    }
}
