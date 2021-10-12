using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Mapping
{
    class AddressOfAccidentMapperProfile : Profile
    {
        public AddressOfAccidentMapperProfile()
        {
            CreateMap<AddressOfAccident, AddressOfAccidentRequestApiModel>().ReverseMap();
            CreateMap<AddressOfAccident, AddressOfAccidentResponseApiModel>().ReverseMap();
        }
    }
}
