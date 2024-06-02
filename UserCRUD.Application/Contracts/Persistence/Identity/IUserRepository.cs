using UserCRUD.Application.Features.User.Query.Get;
using UserCRUD.Domain.Entities.Identity;

namespace UserCRUD.Application.Contracts.Persistence.Identity;

public interface IUserRepository : IRepository<User, Guid, User_Get_Query>{};
