using System.Linq.Expressions;
using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Application.Models.Common;
using UserCRUD.Domain.Entities.Common;
using UserCRUD.Domain.Enum;
using Microsoft.EntityFrameworkCore.Query;

namespace UserCRUD.Application.Contracts.Persistence;

public interface IRepositoryBase<TEntity, in TKey> where TEntity : IEntityBase<TKey>
{
	Task<Paginate<IReadOnlyList<TEntity>>> GetAsync(
		Expression<Func<TEntity, bool>>? predicate = null,
		Tuple<List<IOrderBy>, OrderTypeEnum?>? orderBy = null,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? thenIncludes = null,
        int page = 1,
        int take = 10,
		bool disableTracking = true);

	Task<int> GetCountAsync(
		Expression<Func<TEntity, bool>>? predicate = null,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? thenIncludes = null);

	void Add(TEntity entity);
	void Add(params TEntity[] entities);
	void Add(IEnumerable<TEntity> entities);
	void Update(TEntity entity);
	void Update(params TEntity[] entities);
	void Update(IEnumerable<TEntity> entities);
    void PhysicalDelete(TEntity entity);
    void Delete(TEntity entity);
	void Delete(params TEntity[] entities);
	void Delete(IEnumerable<TEntity> entities);
	Task<int> CommitAsync();
}