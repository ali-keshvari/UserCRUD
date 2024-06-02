using UserCRUD.Application.Models.Common.Http;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.Common.VerifyNationalCode
{
    public class VerifyNationalCode_Response_Dto : Api_ResponseBase_Dto
    {
        public VerifyNationalCode_Response_Dto(ResponseCodeEnum code) : base(code)
        {

        }
    }
}
