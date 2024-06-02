using MediatR;
using UserCRUD.Application.Models.DTOs.User;

namespace UserCRUD.Application.Features.User.Command.Delete
{
    public class User_Delete_Command : IRequest<User_Delete_Response_Dto>
    {
        public Guid Id { get; set; }
    }
}
