using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using UserCRUD.Application.Contracts.Services;
using UserCRUD.Application.Models.Common.Http;
using Newtonsoft.Json;

namespace UserCRUD.Infrastructure.Implementation.Services;
public class HttpService : IHttpService
{
    public async Task<Http_Response_Dto<TData>> GetAsync<TData>(Http_Request_Dto request)
        where TData : class
    {
        using var http = new HttpClient();

        http.BaseAddress = new Uri(request.BaseUrl);
        http.DefaultRequestHeaders.Clear();
        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", MediaTypeNames.Application.Json);

        if (request.Token != null)
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {request.Token}");

        var result = await http.GetAsync(request.Path);

        return new Http_Response_Dto<TData>
        {
            StatusCode = (int)result.StatusCode,
            Succeeded = result.IsSuccessStatusCode,
            Content = await result.Content.ReadFromJsonAsync<TData>()
        };
    }

    public async Task<Http_Response_Dto<TData>> PostAsync<TData>(Http_Request_Dto request)
        where TData : class
    {
        using var http = new HttpClient();

        http.BaseAddress = new Uri(request.BaseUrl);
        http.DefaultRequestHeaders.Clear();
        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", MediaTypeNames.Application.Json);

        if (request.Token != null)
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {request.Token}");

        var jsonData = JsonConvert.SerializeObject(request.Body);
        var content = new StringContent(jsonData, Encoding.UTF8, MediaTypeNames.Application.Json);
        var result = await http.PostAsync(request.Path, content);

        return new Http_Response_Dto<TData>
        {
            StatusCode = (int)result.StatusCode,
            Succeeded = result.IsSuccessStatusCode,
            Content = await result.Content.ReadFromJsonAsync<TData>()
        };
    }
}
