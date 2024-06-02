using UserCRUD.Application.Models.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using UserCRUD.Domain.Entities.Common;
using UserCRUD.Domain.Enum;

namespace UserCRUD.Application.Contracts.Persistence.Common;

public interface ISearchRepository<TEntity, TKey, in TSearch>
    where TEntity : IEntityBase<TKey>
    where TSearch : Base_Paging_Dto<TKey>
{
    Dictionary<string, IOrderBy> OrderFunctions { get; set; }
    Task<TModelList> Search<TModelList, TModelListItem>(TSearch search)
	    where TModelList : Base_List_Dto<TModelListItem>
		where TModelListItem : class;
    Expression<Func<TEntity, bool>>? GetExpression(TSearch search);
    Tuple<List<IOrderBy>, OrderTypeEnum?>? GetOrder(TSearch search);
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? GetIncludes();
}
