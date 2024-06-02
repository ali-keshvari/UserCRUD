using UserCRUD.Application.Models.Common.Http;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.DTOs.Auth;

public abstract class Auth_ResponseBase_Dto : Api_ResponseBase_Dto
{
    public Token_Dto? Tokens { get; set; }
    //public List<RoleEnum>? Roles { get; set; }

    protected Auth_ResponseBase_Dto(ResponseCodeEnum code) : base(code)
    {
    }
}

public class Register_Response_Dto : Api_ResponseBase_Dto
{
    public Register_Response_Dto(ResponseCodeEnum code) : base(code)
    {
    }
}

public class Login_Response_Dto : Auth_ResponseBase_Dto
{
    public Login_Response_Dto(ResponseCodeEnum code) : base(code)
    {
    }
}

public class Refresh_Response_Dto : Auth_ResponseBase_Dto
{
    public Refresh_Response_Dto(ResponseCodeEnum code) : base(code)
    {
    }
}

public class Logout_Response_Dto : Api_ResponseBase_Dto
{
    public Logout_Response_Dto(ResponseCodeEnum code) : base(code)
    {
    }
}
