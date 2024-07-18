using AutoMapper;
using StudioUp.DTO;
using StudioUp.Models;

namespace StudioUp.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContentType, ContentTypeDTO>().ReverseMap();
            CreateMap<ContentSection, ContentSectionDTO>().ReverseMap();
        }
    }
}
