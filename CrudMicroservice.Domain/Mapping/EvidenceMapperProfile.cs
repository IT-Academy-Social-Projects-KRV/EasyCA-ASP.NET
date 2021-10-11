using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Mapping
{
    class EvidenceMapperProfile : Profile
    {
        public EvidenceMapperProfile()
        {
            CreateMap<Evidence, EvidenceRequestApiModel>().ReverseMap();
            CreateMap<Evidence, EvidenceResponseApiModel>().ReverseMap();
        }
    }
}
