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

<<<<<<< HEAD
            CreateMap<TrainingDTO, Training>()
                 .ForMember(dest => dest.TrainerID, opt => opt.MapFrom(src => src.TrainerID))
                 .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
                 //  .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time != null ? new TrainingTime { Hour = src.Time.Hour, Minute = src.Time.Minute } : null))
                 .ForMember(dest => dest.TrainingCustomerTypeId, opt => opt.MapFrom(src => src.TrainingCustomerTypeId))
                 .ForMember(dest => dest.ParticipantsCount, opt => opt.MapFrom(src => src.ParticipantsCount));
            CreateMap<TrainingPostDTO, Training>();
           CreateMap<TrainingPostDTO, Training>()
            .ForMember(dest => dest.TrainerID, opt => opt.MapFrom(src => src.TrainerID))
            .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
            .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.Hour))
            .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => src.Minutes))
            .ForMember(dest => dest.TrainingCustomerTypeId, opt => opt.MapFrom(src => src.TrainingCustomerTypeId))
            .ForMember(dest => dest.ParticipantsCount, opt => opt.MapFrom(src => src.ParticipantsCount))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
=======
         
>>>>>>> f3cfc56cc17eefcf57fbac36c02acf57c3bb8ded
        }
    }
}
