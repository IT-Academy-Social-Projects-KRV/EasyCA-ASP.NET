using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ModelDTO.EntitiesDTO;
using AutoMapper;

namespace AccountService.Domain.Mapping
{
    public class TransportMappingProfile : Profile
    {
        public TransportMappingProfile()
        {
            CreateMap<Transport, TransportDTO>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.CarCategory.CategoryName)).ReverseMap();
            CreateMap<Transport, AddTransportRequestModel>().ReverseMap();
        }
    }
}
