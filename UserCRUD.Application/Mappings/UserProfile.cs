using AutoMapper;
using UserCRUD.Application.Features.User.Command.Create;
using UserCRUD.Application.Features.User.Command.Update;
using UserCRUD.Application.Models.Common.SiteSetting;
using UserCRUD.Application.Models.DTOs.User;
using UserCRUD.Domain.Entities.Identity;

namespace UserCRUD.Application.Mappings;

public class UserProfile : Profile
{
	public UserProfile()
    {

        CreateMap<User, User_List_Item_Dto>().ReverseMap();
        CreateMap<User, User_Create_Command>().ReverseMap();
        CreateMap<User, User_Update_Command>().ReverseMap();
        CreateMap<User_List_Item_Dto, User_Update_Command>().ReverseMap();
        CreateMap<UserSeed, User>().ReverseMap();
	}
}
