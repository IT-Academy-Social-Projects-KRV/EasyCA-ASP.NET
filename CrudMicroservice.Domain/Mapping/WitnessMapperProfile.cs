using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Mapping
{
    class WitnessMapperProfile : Profile
    {
        public WitnessMapperProfile()
        {
            CreateMap<Witness, WitnessRequestApiModel>().ReverseMap();
            CreateMap<Witness, WitnessResponseApiModel>().ReverseMap();
        }
    }
}
