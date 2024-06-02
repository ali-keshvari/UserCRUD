namespace UserCRUD.Application.Models.Common.Http;
public class Http_Request_Dto
{
    public required string BaseUrl { get; set; }
    public required string Path { get; set; }
    public string? Token { get; set; }
    public object? Body { get; set; }
}

public class Http_Request_Dto<TBody> : Http_Request_Dto where TBody : class
{
    public new TBody? Body { get; set; }
}