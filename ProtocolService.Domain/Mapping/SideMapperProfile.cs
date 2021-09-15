using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.RequestApiModels;

namespace ProtocolService.Domain.Mapping
{
    public class SideMapperProfile: Profile
    {
        public SideMapperProfile()
        {
            CreateMap<Side, SideRequestModel>().ReverseMap();
        }
    }
}
