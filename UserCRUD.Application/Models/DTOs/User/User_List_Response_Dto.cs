using UserCRUD.Application.Models.Common.Http;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.DTOs.User;

public class User_List_Response_Dto : Api_ResponseBase_Dto
{
    public List<User_List_Item_Dto> Users { get; set; } = new();

    public User_List_Response_Dto(ResponseCodeEnum? code = ResponseCodeEnum.Ok) : base(code)
    {
    }
}
