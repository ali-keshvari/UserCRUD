using System.Linq.Expressions;
using UserCRUD.Application.Common.Exceptions;
using UserCRUD.Application.Contracts.Persistence;
using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Application.Models.Common;
using UserCRUD.Domain.Entities.Common;
using UserCRUD.Domain.Enum;
using UserCRUD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace UserCRUD.Infrastructure.Implementation.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
	where TEntity : class, IEntityBase<TKey>
{
	protected readonly AppDbContext Context;
	protected readonly DbSet<TEntity> DbSet;

    public RepositoryBase(AppDbContext context)
	{
		Context = context;
		DbSet = Context.Set<TEntity>();
	}

	#region Get

	public async Task<Paginate<IReadOnlyList<TEntity>>> GetAsync(
		Expression<Func<TEntity, bool>>? predicate = null,
		Tuple<List<IOrderBy>, OrderTypeEnum?>? orderBy = null,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? thenIncludes = null,
        int page = 1,
        int take = 10,
        bool disableTracking = true)
	{
		try
		{
			var query = DbSet.AsQueryable();
			if (disableTracking) query = query.AsNoTracking();
			if (thenIncludes != null) query = thenIncludes(query);
			if (predicate != null) query = query.Where(predicate);
			if (orderBy != null)
			{
				foreach (var orderByItem in orderBy.Item1)
				{
					query = orderBy.Item2 == OrderTypeEnum.Descending ?
						Queryable.OrderByDescending(query, orderByItem.Expression) :
						Queryable.OrderBy(query, orderByItem.Expression);
				}
			}

            var count = query.Count();
            var skip = 0;

            if (count >= 0 && take == 0)
            {
                take = int.MaxValue;
            }

            if (page == 0)
            {
                page = 1;
            }

            if (count > 0)
            {
                skip = (page - 1) * take;
                query = query.Skip(skip).Take(take);
            }

            var result = new Paginate<IReadOnlyList<TEntity>>
            {
                Items = await query.ToListAsync(),
                TotalCount = count,
				CurrentPage = page,
				FirstPage = 1,
				LastPage = count > take ? Convert.ToInt32(Math.Floor(Convert.ToDecimal(count / take) + 1)) : 1,
				HasPrevious = page > 1
			};

            return result;
        }
		catch (Exception ex)
		{
			throw new ContextException(typeof(TEntity).Name, ex);
		}
	}

	public async Task<int> GetCountAsync(
		Expression<Func<TEntity, bool>>? predicate = null,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? thenIncludes = null)
	{
		try
		{
			var query = DbSet.AsQueryable();
			if (thenIncludes != null) query = thenIncludes(query);
			if (predicate != null) query = query.Where(predicate);

			return await query.CountAsync();
		}
		catch (Exception ex)
		{
			throw new ContextException(typeof(TEntity).Name, ex);
		}
	}

	#endregion

	#region Add

	public void Add(TEntity entity)
	{
		try
		{
			Context.Entry(entity).State = EntityState.Added;
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	public void Add(params TEntity[] entities)
	{
		try
		{
			foreach (var entity in entities)
			{
				Context.Entry(entity).State = EntityState.Added;
			}
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	public void Add(IEnumerable<TEntity> entities)
	{
		try
		{
			foreach (var entity in entities)
			{
				Context.Entry(entity).State = EntityState.Added;
			}
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	#endregion

	#region Update

	public void Update(TEntity entity)
	{
		try
		{
			Context.Entry(entity).State = EntityState.Modified;
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	public void Update(params TEntity[] entities)
	{
		try
		{
			Context.Entry(entities).State = EntityState.Modified;
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	public void Update(IEnumerable<TEntity> entities)
	{
		try
		{
			Context.Entry(entities).State = EntityState.Modified;
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	#endregion

	#region Delete

    public void PhysicalDelete(TEntity entity)
    {
        try
        {
            Context.Remove(entity);
        }
        catch (Exception e)
        {
            throw new ContextException(typeof(TEntity).Name, e);
        }
    }


    public void Delete(TEntity entity)
	{
		try
		{
			entity.IsDeleted = true;
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	public void Delete(params TEntity[] entities)
	{
		try
		{
			entities.ToList().ForEach(e => e.IsDeleted = true);
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	public void Delete(IEnumerable<TEntity> entities)
	{
		try
		{
			entities.ToList().ForEach(e => e.IsDeleted = true);
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	#endregion

	#region Save

	public Task<int> CommitAsync()
	{
		try
		{
			return Context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			throw new ContextException(typeof(TEntity).Name, e);
		}
	}

	#endregion
}