using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;

namespace ProtocolService.Domain.Mapping
{
    class EvidenceMapperProfile : Profile
    {
        public EvidenceMapperProfile()
        {
            CreateMap<Evidence, EvidenceRequestModel>().ReverseMap();
            CreateMap<EuroProtocol, EvidenceResponseModel>().ReverseMap();
        }
    }
}
