using Microsoft.AspNetCore.Http;
using UserCRUD.Application.Contracts.Services;

namespace UserCRUD.Infrastructure.Implementation.Services
{
    public class ValidFilesService : IValidFilesService
    {
        public async Task<bool> IsValid(IEnumerable<IFormFile> multipleFiles)
        {
            foreach (var file in multipleFiles)
            {
                if (file.Length > 512000)
                {
                    return false;
                }
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!new[] { ".txt", ".pdf", ".doc", ".docx" }.Contains(extension))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
