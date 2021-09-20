using AutoMapper;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.ResponceApiModels;

namespace ProtocolService.Domain.Mapping
{
    class AddressOfAccidentMapperProfile : Profile
    {
        public AddressOfAccidentMapperProfile()
        {
            CreateMap<AddressOfAccident, AddressOfAccidentResponseModel>().ReverseMap();
        }
    }
}
