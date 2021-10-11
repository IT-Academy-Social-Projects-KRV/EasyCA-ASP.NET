using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using AutoMapper;

namespace CrudMicroservice.Domain.Mapping
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserResponseApiModel>().ReverseMap();
            CreateMap<UserRequestApiModel, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, InspectorRequestApiModel>().ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); ;
        }
    }
}
