using System.Linq.Expressions;
using UserCRUD.Application.Contracts.Persistence.Common;

namespace UserCRUD.Infrastructure.Implementation.Common;

public class OrderBy<TEntity, TProperty> : IOrderBy
{
    private readonly Expression<Func<TEntity, TProperty>> _expression;

    public OrderBy(Expression<Func<TEntity, TProperty>> expression)
    {
        _expression = expression;
    }

    public dynamic Expression => _expression;
}
