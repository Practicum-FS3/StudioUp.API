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
            CreateMap<Training, TrainingDTO>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));

            CreateMap<TrainingTime, TrainingTimeDTO>().ReverseMap();
            CreateMap<TrainingDTO, Training>()
                 .ForMember(dest => dest.TrainerID, opt => opt.MapFrom(src => src.TrainerID))
                 .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
                 .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time != null ? new TrainingTime { Hour = src.Time.Hour, Minute = src.Time.Minute } : null))
                 .ForMember(dest => dest.TrainingCustomerTypeId, opt => opt.MapFrom(src => src.TrainingCustomerTypeId))
                 .ForMember(dest => dest.ParticipantsCount, opt => opt.MapFrom(src => src.ParticipantsCount))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
            CreateMap<trainingPostDTO, Training>()
                 .ForMember(dest => dest.ID, opt => opt.Ignore()) // לא נדרש למיפוי בהוספה חדשה
                 .ForMember(dest => dest.TrainerID, opt => opt.MapFrom(src => src.TrainerID))
                 .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
                 .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time != null ? new TrainingTime { Hour = src.Time.Hour, Minute = src.Time.Minute } : null))
                 .ForMember(dest => dest.TrainingCustomerTypeId, opt => opt.MapFrom(src => src.TrainingCustomerTypeId))
                 .ForMember(dest => dest.ParticipantsCount, opt => opt.MapFrom(src => src.ParticipantsCount))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

            //CreateMap<TrainingDTO, Training>()
            //    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
        }
    }
}
