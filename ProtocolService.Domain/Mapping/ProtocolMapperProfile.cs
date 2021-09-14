using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.RequestApiModels;

namespace ProtocolService.Domain.Mapping
{
    public class ProtocolMapperProfile: Profile
    {
        public ProtocolMapperProfile()
        {
            CreateMap<EuroProtocol, EuroProtocolRequestModel>().ReverseMap();
        }
    }
}
