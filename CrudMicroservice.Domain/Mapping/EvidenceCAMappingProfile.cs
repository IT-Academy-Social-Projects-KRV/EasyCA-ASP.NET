using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Mapping
{
    public class EvidenceCAMappingProfile: Profile
    {
        public EvidenceCAMappingProfile()
        {
            CreateMap<EvidenceCA, EvidenceCARequestApiModel>().ReverseMap();
            CreateMap<EvidenceCA, EvidenceCAResponseApiModel>().ReverseMap();
        }
    }
}
