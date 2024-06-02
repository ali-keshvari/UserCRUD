using UserCRUD.Application.Models.Common.Http;

namespace UserCRUD.Application.Contracts.Services;
public interface IHttpService
{
    Task<Http_Response_Dto<TData>> GetAsync<TData>(Http_Request_Dto request)
        where TData : class;

    Task<Http_Response_Dto<TData>> PostAsync<TData>(Http_Request_Dto request)
        where TData : class;
}
