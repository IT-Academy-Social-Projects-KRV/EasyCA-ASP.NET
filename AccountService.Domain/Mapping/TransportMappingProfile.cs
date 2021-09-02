using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AutoMapper;

namespace AccountService.Domain.Mapping
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
