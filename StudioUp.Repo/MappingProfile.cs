using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.Repositories;

namespace StudioUp.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<AvailableTraining, AvailableTrainingDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<CustomerType, CustomerTypeDTO>().ReverseMap();
            CreateMap<HMO, HMODTO>().ReverseMap();
            CreateMap<DTO.PaymentOptionDTO, DTO.PaymentOptionDTO>().ReverseMap();
            CreateMap<Models.SubscriptionType, SubscriptionTypeDTO>().ReverseMap();
            CreateMap<Trainer, TrainerDTO>().ReverseMap();
            CreateMap<TrainingCustomer, TrainingCustomerDTO>().ReverseMap();
            CreateMap<TrainingCustomerType, TrainingCustomerTypeDTO>().ReverseMap();

            CreateMap<Training, TrainingDTO>().ReverseMap();
            CreateMap<TrainingType, TrainingTypeDTO>().ReverseMap();
            CreateMap<FileUpload, FileUploadDTO>().ReverseMap();
            CreateMap<FileUpload, FileDownloadDTO>().ReverseMap();
            CreateMap<CustomerHMOS, CustomerHMOSDTO>().ReverseMap();
            //CreateMap<Training>
            //CreateMap<SubscriptionRoutes, SubscriptionRoutesDTO>().ReverseMap();
            CreateMap<LeumitCommitments, LeumitCommitmentsDTO>().ReverseMap();
            CreateMap<LeumitCommimentTypes, LeumitCommimentTypesDTO>().ReverseMap();
            CreateMap<Training, CalanderTrainingDTO>()
                  .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.FirstName + " " + src.Trainer.LastName))
                  .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => string.Format("{0}:{1}", src.Hour, src.Minute)))
            .ForMember(dest => dest.CustomerTypeName, opt => opt.MapFrom(src => src.TrainingCustomerType.CustomerType.Title))
            .ForMember(dest => dest.TrainingTypeName, opt => opt.MapFrom(src => src.TrainingCustomerType.TrainingType.Title));





            CreateMap<AvailableTraining, CalanderAvailableTrainingDTO>()
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Training.Trainer.FirstName + " " + src.Training.Trainer.LastName))
            .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.Training.DayOfWeek))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => string.Format("{0}:{1}", src.Training.Hour, src.Training.Minute)))
            .ForMember(dest => dest.CustomerTypeName, opt => opt.MapFrom(src => src.Training.TrainingCustomerType.CustomerType.Title))
            .ForMember(dest => dest.TrainingTypeName, opt => opt.MapFrom(src => src.Training.TrainingCustomerType.TrainingType.Title));

            CreateMap<TrainingCustomer, CalanderAvailableTrainingDTO>()
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => string.Format("{0} {1}", src.Training.Training.Trainer.FirstName + " " + src.Training.Training.Trainer.LastName)))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => string.Format("{0}:{1}", src.Training.Training.Hour, src.Training.Training.Minute)))
            .ForMember(dest => dest.CustomerTypeName, opt => opt.MapFrom(src => src.Customer.CustomerType.Title))
            .ForMember(dest => dest.TrainingTypeName, opt => opt.MapFrom(src => src.Training.Training.TrainingCustomerType.TrainingType.Title))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Training.Date))
            .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => (int)src.Training.Date.DayOfWeek))
            .ForMember(dest => dest.ParticipantsCount, opt => opt.MapFrom(src => src.Training.ParticipantsCount));
        }
    }
}

