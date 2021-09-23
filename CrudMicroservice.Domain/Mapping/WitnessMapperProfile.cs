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
            CreateMap<Witness, WitnessRequestModel>().ReverseMap();
            CreateMap<Witness, WitnessResponseModel>().ReverseMap();
        }
    }
}
