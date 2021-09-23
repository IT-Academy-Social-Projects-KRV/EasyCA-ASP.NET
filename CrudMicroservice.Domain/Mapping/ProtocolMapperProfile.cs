using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Mapping
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
