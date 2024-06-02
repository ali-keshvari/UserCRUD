namespace UserCRUD.Application.Common.Exceptions;

public class HttpException : ExceptionBase
{
    public HttpException(string message, int? statusCode = 500, IReadOnlyDictionary<string, string[]>? errors = null) 
        : base(message, errors)
    {
        if (statusCode != null) StatusCode = (int)statusCode;
        Title = nameof(HttpException);
    }
}
