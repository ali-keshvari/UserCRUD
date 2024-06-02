using UserCRUD.Application.Models.Common.Http;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.DTOs.File
{
    public class File_List_Response_Dto : Api_ResponseBase_Dto
    {
        public List<File_List_Item_Dto> Files { get; set; } = new();

        public File_List_Response_Dto(ResponseCodeEnum? code = ResponseCodeEnum.Ok) : base(code)
        {
        }
    }
}
