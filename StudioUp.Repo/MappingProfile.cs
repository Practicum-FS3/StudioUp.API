using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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

            CreateMap<PaymentOptionDTO,PaymentOptionDTO>().ReverseMap();
            CreateMap<SubscriptionType, SubscriptionTypeDTO>().ReverseMap();
            CreateMap<Trainer, TrainerDTO>().ReverseMap();
            CreateMap<TrainingCustomer, TrainingCustomerDTO>().ReverseMap();
            CreateMap<TrainingCustomerType, TrainingCustomerTypeDTO>().ReverseMap();
            CreateMap<CustomerSubscriptionDTO, CustomerSubscription>().ReverseMap();
            CreateMap<TrainingCustomer, TrainingCustomerDTO>().ReverseMap();


            CreateMap<ContentType, ContentTypeDTO>().ReverseMap();
            CreateMap<ContentSection, ContentSectionDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new FileDownloadDTO
                {
                    FileName = src.ContentTypeID.ToString() + " " + src.ID.ToString(),
                    ContentType = "image/png", // ערך קבוע
                    Data = src.ImageData,
                }));

            CreateMap<ContentSectionManagementDTO, ContentSection>()
             .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => ConvertIFormFileToByteArray(src.fileUploadDTO)));

            CreateMap<TrainingCustomerType, TrainingCustomerTypeDTO>()
               .ForMember(dest => dest.TrainingCustomerName, opt => opt.MapFrom(src => src.TrainingType.Title + " " + src.CustomerType.Title));
            CreateMap<TrainingCustomerTypePostComand, TrainingCustomerType>().ReverseMap();
            CreateMap<Training, TrainingDTO>().ReverseMap();
            CreateMap<TrainingType, TrainingTypeDTO>().ReverseMap();
            CreateMap<FileUpload, FileUploadDTO>().ReverseMap();
            CreateMap<FileUpload, FileDownloadDTO>().ReverseMap();
            CreateMap<CustomerHMOS, CustomerHMOSDTO>().ReverseMap();
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


            CreateMap<Training, TrainingDTO>()
                .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.Hour.ToString("D2")))
                .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => src.Minute.ToString("D2")));
            CreateMap<TrainingDTO, Training>()
                .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => int.Parse(src.Hour)))
                .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => int.Parse(src.Minute)));
            CreateMap<TrainingPostDTO, Training>()
                 .ForMember(dest => dest.ID, opt => opt.Ignore())
                 .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => int.Parse(src.Hour)))
                 .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => int.Parse(src.Minute)));
            CreateMap<Training, TrainingPostDTO>()
                .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.Hour.ToString("D2")))
                .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => src.Minute.ToString("D2")));
            CreateMap<TrainingDTO, TrainingPostDTO>()
                .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.Hour))
                .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => src.Minute));
            CreateMap<TrainingPostDTO, TrainingDTO>()
                .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.Hour))
                .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => src.Minute));



            CreateMap<TrainingCustomer, CalanderAvailableTrainingDTO>()
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Training.Training.Trainer != null ? $"{src.Training.Training.Trainer.FirstName} {src.Training.Training.Trainer.LastName}" : string.Empty))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => string.Format("{0}:{1}", src.Training.Training.Hour, src.Training.Training.Minute)))
            .ForMember(dest => dest.CustomerTypeName, opt => opt.MapFrom(src => src.Customer.CustomerType.Title))
            .ForMember(dest => dest.TrainingTypeName, opt => opt.MapFrom(src => src.Training.Training.TrainingCustomerType.TrainingType.Title))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Training.Date))
            .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.Training.Training.DayOfWeek))
            .ForMember(dest => dest.ParticipantsCount, opt => opt.MapFrom(src => src.Training.ParticipantsCount));
        }
        private byte[] ConvertIFormFileToByteArray(FileUploadDTO file)
        {
            if (file == null)
                return null;


            using (var memoryStream = new MemoryStream())
            {
                file.File.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }

            .ForMember(dest => dest.ParticipantsCount, opt => opt.MapFrom(src => src.Training.ParticipantsCount))
            .ForMember(dest => dest.IsRegistered, opt => opt.MapFrom(src => true));

        }
    }
}

