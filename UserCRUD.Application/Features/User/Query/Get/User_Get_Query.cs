using UserCRUD.Application.Models.Common;
using UserCRUD.Application.Models.DTOs.User;
using MediatR;

namespace UserCRUD.Application.Features.User.Query.Get;

public class User_Get_Query : Base_Paging_Dto<Guid>, IRequest<User_List_Response_Dto>
{
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PersonalCode { get; set; }
    public string? NationalCode { get; set; }
    public Guid? DeniedId{ get; set; }
}
