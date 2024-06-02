using System.Linq.Expressions;
using AutoMapper;
using UserCRUD.Application.Contracts.Persistence;
using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Application.Models.Common;
using UserCRUD.Domain.Entities.Common;
using UserCRUD.Domain.Enum;
using UserCRUD.Infrastructure.Data.Context;
using UserCRUD.Infrastructure.Implementation.Repositories;
using Microsoft.EntityFrameworkCore.Query;

namespace UserCRUD.Infrastructure.Implementation;

public abstract class Repository<TEntity, TKey, TSearch>
    : RepositoryBase<TEntity, TKey>,
        IRepository<TEntity, TKey, TSearch>
    where TEntity : class, IEntityBase<TKey>
    where TSearch : Base_Paging_Dto<TKey>
{
    private readonly IMapper? _mapper;

    protected Repository(AppDbContext context)
        : base(context)
    {
    }

    protected Repository(AppDbContext context, IMapper mapper)
        : base(context)
    {
        _mapper = mapper;
    }

    public abstract Dictionary<string, IOrderBy> OrderFunctions { get; set; }

    public async Task<TEntity?> GetByPropertyAsync(TSearch search)
    {
        return (await GetAsync(
                    predicate: GetExpression(search),
                    disableTracking: false))
            .Items.FirstOrDefault();
    }

    public virtual async Task<TModelList> Search<TModelList, TModelListItem>(TSearch search)
        where TModelList : Base_List_Dto<TModelListItem>
        where TModelListItem : class
    {
        var output = Activator.CreateInstance<TModelList>();

        var list = await GetAsync(
            predicate: GetExpression(search),
            orderBy: GetOrder(search),
            thenIncludes: GetIncludes(),
            page: search.PageNum,
            take: search.Limit,
            disableTracking: true);

        output.TotalCount = list.Items.Count;

        var mapper = new MapperConfiguration(cfg
            => cfg.CreateMap<TEntity, TModelListItem>())
            .CreateMapper();

        if (_mapper != null)
        {
            mapper = _mapper;
        }

        foreach (var item in list.Items)
        {
            var newItem = Activator.CreateInstance<TModelListItem>();
            mapper.Map(item, newItem);
            output.Rows.Add(newItem);
        }

        output.TotalCount = list.TotalCount;
        output.CurrentPage = list.CurrentPage;
        output.FirstPage = list.FirstPage;
        output.LastPage = list.LastPage;
        output.HasNext = list.HasNext;
        output.HasPrevious = list.HasPrevious;

        return output;
    }

    public abstract Expression<Func<TEntity, bool>>? GetExpression(TSearch search);
    public abstract Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? GetIncludes();

    public virtual Tuple<List<IOrderBy>, OrderTypeEnum?>? GetOrder(TSearch search)
    {
        if (search.OrderByList == null) return null;

        var orderByList = new List<IOrderBy>();

        foreach (var item in search.OrderByList)
        {
            var isExist = OrderFunctions.TryGetValue(item.ToLower(), out IOrderBy? orderExpression);

            if (!isExist || orderExpression == null) continue;

            orderByList.Add(orderExpression);
        }

        return Tuple.Create(orderByList, search.OrderType);
    }
}