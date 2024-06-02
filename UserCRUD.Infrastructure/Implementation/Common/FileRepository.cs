using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using UserCRUD.Application.Common.Utils.Common;
using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Application.Features.File.Query.Get;
using UserCRUD.Domain.Entities.Common;
using UserCRUD.Infrastructure.Data.Context;

namespace UserCRUD.Infrastructure.Implementation.Common
{
    public class FileRepository : Repository<Upload_File, Guid, File_Get_Query>, IFileRepository
    {
        public FileRepository(AppDbContext context) : base(context)
        {
        }

        public FileRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override Dictionary<string, IOrderBy> OrderFunctions { get; set; }
        public override Expression<Func<Upload_File, bool>>? GetExpression(File_Get_Query search)
        {
            Expression<Func<Upload_File, bool>>? condition = null;

            condition = condition.And(f => !f.IsDeleted);

            if (search.Id != null && search.Id != Guid.Empty)
            {
                condition = condition.And(f => Equals(f.Id, search.Id));
            }

            if (search.UserId != null && search.UserId != Guid.Empty)
            {
                condition = condition.And(f => Equals(f.UserId, search.UserId));
            }

            return condition;
        }

        public override Func<IQueryable<Upload_File>, IIncludableQueryable<Upload_File, object>>? GetIncludes() => null;
    }
}
