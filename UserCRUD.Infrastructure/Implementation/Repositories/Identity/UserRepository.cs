using UserCRUD.Application.Common.Utils.Common;
using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Application.Contracts.Persistence.Identity;
using UserCRUD.Application.Features.User.Query.Get;
using UserCRUD.Domain.Entities.Identity;
using UserCRUD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using AutoMapper;
using UserCRUD.Infrastructure.Implementation.Common;

namespace UserCRUD.Infrastructure.Implementation.Repositories.Identity;

public class UserRepository : Repository<User, Guid, User_Get_Query>, IUserRepository
{
    public UserRepository(AppDbContext context)
        : base(context)
    {
        AddOrderFunctions();
    }

    private void AddOrderFunctions()
    {
        OrderFunctions = new()
        {
            {nameof(User.CreatedAt).ToLower(), new OrderBy<User, DateTime>(e => e.CreatedAt)},
            {nameof(User.PersonalCode).ToLower(), new OrderBy<User, string>(e => e.PersonalCode)}
        };
    }

    public override Dictionary<string, IOrderBy> OrderFunctions { get; set; }

	public override Expression<Func<User, bool>>? GetExpression(User_Get_Query search)
	{
        Expression<Func<User, bool>>? condition = null;
        condition = condition.And(u => !u.IsDeleted);

        if (search.Id != null && search.Id != Guid.Empty)
        {
            condition = condition.And(u => Equals(u.Id, search.Id));
        }
        if (!string.IsNullOrEmpty(search.FirstName))
        {
            condition = condition.And(u => Equals(u.FirstName, search.FirstName));
        }
        if (!string.IsNullOrEmpty(search.LastName))
        {
            condition = condition.And(u => Equals(u.LastName, search.LastName));
        }
        if (!string.IsNullOrEmpty(search.PersonalCode))
        {
            condition = condition.And(u => Equals(u.PersonalCode, search.PersonalCode));
        }
        if (!string.IsNullOrEmpty(search.NationalCode))
        {
            condition = condition.And(u => Equals(u.NationalCode, search.NationalCode));
        }
        return condition;
    }
    public override Func<IQueryable<User>, IIncludableQueryable<User, object>>? GetIncludes()
        => null;
}
