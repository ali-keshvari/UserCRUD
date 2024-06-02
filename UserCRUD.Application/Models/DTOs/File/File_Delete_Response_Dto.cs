using UserCRUD.Application.Models.Common.Http;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.DTOs.File
{
    public class File_Delete_Response_Dto : Api_ResponseBase_Dto
    {
        public File_Delete_Response_Dto(ResponseCodeEnum code) : base(code)
        {
        }
    }
}
