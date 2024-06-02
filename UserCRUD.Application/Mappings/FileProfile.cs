using AutoMapper;
using UserCRUD.Application.Features.File.Command.Create;
using UserCRUD.Application.Models.DTOs.File;
using UserCRUD.Domain.Entities.Common;

namespace UserCRUD.Application.Mappings
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<Upload_File, File_List_Item_Dto>().ReverseMap();
            CreateMap<Upload_File, File_Create_Command>().ReverseMap();
        }
    }
}
