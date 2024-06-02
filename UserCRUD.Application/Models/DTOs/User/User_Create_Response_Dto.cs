using UserCRUD.Application.Models.Common.Http;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.DTOs.User
{
    public class User_Create_Response_Dto : Api_ResponseBase_Dto
    {
        public User_Create_Response_Dto(ResponseCodeEnum code) : base(code)
        {
        }
    }
}
