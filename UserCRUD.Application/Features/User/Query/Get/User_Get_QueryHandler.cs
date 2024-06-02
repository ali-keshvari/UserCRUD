using AutoMapper;
using UserCRUD.Application.Contracts.Persistence.Identity;
using UserCRUD.Application.Models.DTOs.User;
using MediatR;

namespace UserCRUD.Application.Features.User.Query.Get;

public class User_Get_QueryHandler : IRequestHandler<User_Get_Query, User_List_Response_Dto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public User_Get_QueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<User_List_Response_Dto> Handle(User_Get_Query request, CancellationToken cancellationToken)
    {
        var res = new User_List_Response_Dto();

        var users = await _userRepository.Search<User_List_Dto, User_List_Item_Dto>(request);
        
        res.Users.AddRange(users.Rows);
        
        _mapper.Map(users, res);

        return res;
    }
}
