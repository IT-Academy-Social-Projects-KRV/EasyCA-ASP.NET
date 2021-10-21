using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Mapping
{
    public class SideCAMappingProfile:Profile
    {
        public SideCAMappingProfile()
        {
            CreateMap<SideCA, SideCARequestApiModel>().ReverseMap();
            CreateMap<SideCA, SideCAResponseApiModel>().ReverseMap();
        }
    }
}
