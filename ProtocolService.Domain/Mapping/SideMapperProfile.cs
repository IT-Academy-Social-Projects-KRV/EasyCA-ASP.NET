using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;

namespace ProtocolService.Domain.Mapping
{
    public class SideMapperProfile : Profile
    {
        public SideMapperProfile()
        {
            CreateMap<Side, SideRequestModel>().ReverseMap();
            CreateMap<Side, SideResponseModel>().ReverseMap();
        }
    }
}
