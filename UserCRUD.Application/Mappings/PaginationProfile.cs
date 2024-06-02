using AutoMapper;
using UserCRUD.Application.Models.Common;
using UserCRUD.Application.Models.Common.Http;

namespace UserCRUD.Application.Mappings;

public class PaginationProfile : Profile
{
	public PaginationProfile()
	{
		CreateMap(typeof(Base_List_Dto<>), typeof(Api_ResponseBase_Dto));
    }
}
