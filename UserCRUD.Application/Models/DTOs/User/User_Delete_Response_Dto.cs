using UserCRUD.Application.Models.Common.Http;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.DTOs.User
{
    public class User_Delete_Response_Dto : Api_ResponseBase_Dto
    {
        public User_Delete_Response_Dto(ResponseCodeEnum code) : base(code)
        {
        }
    }
}
