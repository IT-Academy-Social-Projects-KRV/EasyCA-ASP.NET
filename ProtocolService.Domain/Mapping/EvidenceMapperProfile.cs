using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.ResponceApiModels;

namespace ProtocolService.Domain.Mapping
{
    class EvidenceMapperProfile : Profile
    {
        public EvidenceMapperProfile()
        {
            CreateMap<Evidence, EvidenceResponseModel>().ReverseMap();
        }
    }
}
