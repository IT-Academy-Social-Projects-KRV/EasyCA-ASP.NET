using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;

namespace ProtocolService.Domain.Mapping
{
    public class AddressMapperProfile : Profile
    {
        public AddressMapperProfile()
        {
            CreateMap<AddressOfAccident, AddressOfAccidentRequestModel>().ReverseMap();
            CreateMap<AddressOfAccident, AddressOfAccidentResponseModel>().ReverseMap();
        }
    }
}
