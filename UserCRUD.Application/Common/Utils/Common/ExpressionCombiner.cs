using System.Linq.Expressions;

namespace UserCRUD.Application.Common.Utils.Common;

public static class ExpressionCombiner
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>>? exp, Expression<Func<T, bool>> newExp)
    {
        if (exp == null && newExp != null)
        {
            return newExp;
        }
        var visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());
        newExp = visitor.Visit(newExp) as Expression<Func<T, bool>>;

        var binExp = Expression.And(exp.Body, newExp.Body);
        return Expression.Lambda<Func<T, bool>>(binExp, newExp.Parameters);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> newExp)
    {
        if (exp == null && newExp != null)
        {
            return newExp;
        }
        var visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());
        newExp = visitor.Visit(newExp) as Expression<Func<T, bool>>;

        var binExp = Expression.Or(exp.Body, newExp.Body);
        return Expression.Lambda<Func<T, bool>>(binExp, newExp.Parameters);
    }
}

class ParameterUpdateVisitor : ExpressionVisitor
{
    private readonly ParameterExpression _oldParameter;
    private readonly ParameterExpression _newParameter;

    public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
    {
        _oldParameter = oldParameter;
        _newParameter = newParameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return ReferenceEquals(node, _oldParameter) ? _newParameter : base.VisitParameter(node);
    }
}