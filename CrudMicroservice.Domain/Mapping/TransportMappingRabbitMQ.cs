using AutoMapper;
using CrudMicroservice.Data.Entities;
using RabbitMQConfig.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudMicroservice.Domain.Mapping
{

    public class TransportMappingRabbitMQ:Profile
    {
        public TransportMappingRabbitMQ()
        {
            CreateMap<Transport, TransportResponseModelRabbitMQ>()
               .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.CarCategory.CategoryName))
               .ForMember(x => x.InsuaranceNumber, opt => opt.MapFrom(src => new InsuaranceModelRabbitMQ()
               {
                   CompanyName = src.InsuaranceNumber.CompanyName,
                   SerialNumber = src.InsuaranceNumber.SerialNumber
               }));
         
        }       
    }
}
