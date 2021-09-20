using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;

namespace ProtocolService.Domain.Mapping
{
    public class ProtocolMapperProfile : Profile
    {
        public ProtocolMapperProfile()
        {
            CreateMap<EuroProtocol, EuroProtocolRequestModel>().ReverseMap();
            CreateMap<EuroProtocol, EuroProtocolResponseModel>().ReverseMap();
        }
    }
}
