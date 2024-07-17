using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using StudioUp.DTO;
using StudioUp.Models;

namespace StudioUp.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<AvailableTraining, AvailableTrainingDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<CustomerType, CustomerTypeDTO>().ReverseMap();
            CreateMap<HMO, HMODTO>().ReverseMap();
            CreateMap<PaymentOption, PaymentOptionDTO>().ReverseMap();
            CreateMap<SubscriptionType, SubscriptionTypeDTO>().ReverseMap();
            CreateMap<Trainer, TrainerDTO>().ReverseMap();
            CreateMap<TrainingCustomer, TrainingCustomerDTO>().ReverseMap();
            CreateMap<Training, TrainingDTO>().ReverseMap();
            CreateMap<TrainingType, TrainingTypeDTO>().ReverseMap();

        }
    }
}

