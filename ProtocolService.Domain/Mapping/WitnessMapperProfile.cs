using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.ResponceApiModels;

namespace ProtocolService.Domain.Mapping
{
    class WitnessMapperProfile : Profile
    {
        public WitnessMapperProfile()
        {
            CreateMap<Witness, WitnessResponseModel>().ReverseMap();
        }
    }
}
