using UserCRUD.Application.Features.File.Query.Get;
using UserCRUD.Domain.Entities.Common;

namespace UserCRUD.Application.Contracts.Persistence.Common
{
    public interface IFileRepository : IRepository<Upload_File,Guid,File_Get_Query>
    {
    }
}
