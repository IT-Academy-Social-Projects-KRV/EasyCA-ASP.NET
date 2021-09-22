using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using AutoMapper;

namespace CrudMicroservice.Domain.Mapping
{
    public class TransportMappingProfile : Profile
    {
        public TransportMappingProfile()
        {
            CreateMap<Transport, TransportResponseApiModel>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.CarCategory.CategoryName)).ReverseMap();
            CreateMap<Transport, AddTransportRequestModel>().ReverseMap();
        }
    }
}
