using UserCRUD.Application.Common.Extensions;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Models.Common.Http;

public class Api_ResponseBase_Dto
{
    public Api_ResponseBase_Dto()
    {
        ResponseCode = ResponseCodeEnum.Ok;
    }

    public Api_ResponseBase_Dto(ResponseCodeEnum? code = ResponseCodeEnum.Ok)
    {
        ResponseCode = code;
    }

    public bool Ok => ResponseCode is ResponseCodeEnum.Ok;

    private ResponseCodeEnum? _responseCode;
    public ResponseCodeEnum? ResponseCode
    {
        get => _responseCode;
        set
        {
            _responseCode = value;
            Messages ??= new();
            Messages = _responseCode.ToResponseMessages();
        }
    }

    public Dictionary<string, string>? Messages { get; set; }

    public int TotalCount { get; set; }
    public int FirstPage { get; set; }
    public int LastPage { get; set; }
    public int CurrentPage { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
}
