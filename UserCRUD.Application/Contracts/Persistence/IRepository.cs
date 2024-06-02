using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Application.Models.Common;
using UserCRUD.Domain.Entities.Common;

namespace UserCRUD.Application.Contracts.Persistence;

public interface IRepository<TEntity, TKey, in TSearch>
	: IRepositoryBase<TEntity, TKey>,
		ISearchRepository<TEntity, TKey, TSearch>
	where TEntity : IEntityBase<TKey>
	where TSearch : Base_Paging_Dto<TKey>
{
	Task<TEntity?> GetByPropertyAsync(TSearch search);
}