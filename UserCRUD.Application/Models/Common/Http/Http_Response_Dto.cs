namespace UserCRUD.Application.Models.Common.Http;
public class Http_Response_Dto<TModel>
{
    public int StatusCode { get; set; }
    public bool Succeeded { get; set; }
    public TModel? Content { get; set; }
}
