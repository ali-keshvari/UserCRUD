using UserCRUD.Application.Models.Common.Http;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.Common.Email;

public class EmailSendResponseDto : Api_ResponseBase_Dto
{
    public EmailSendResponseDto(ResponseCodeEnum code) : base(code)
    {
    }
}
