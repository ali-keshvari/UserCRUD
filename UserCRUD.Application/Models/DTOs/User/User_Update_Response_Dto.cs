using UserCRUD.Application.Models.Common.Http;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.DTOs.User
{
    public class User_Update_Responce_Dto : Api_ResponseBase_Dto
    {
        public User_Update_Responce_Dto(ResponseCodeEnum code) : base(code)
        {
        }
    }
}
