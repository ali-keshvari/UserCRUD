using Microsoft.AspNetCore.Http;

namespace UserCRUD.Application.Contracts.Services
{
    public interface IValidFilesService
    {
        Task<bool> IsValid(IEnumerable<IFormFile> multipleFiles);
    }
}
