using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;

namespace ProtocolService.Domain.Mapping
{
    class WitnessMapperProfile:Profile
    {
        public WitnessMapperProfile()
        {
            CreateMap<Witness, WitnessRequestModel>().ReverseMap();
            CreateMap<Witness, WitnessResponseModel>().ReverseMap();
        }
    }
}   
