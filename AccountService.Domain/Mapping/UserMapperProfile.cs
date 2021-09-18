using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AutoMapper;

namespace AccountService.Domain.Mapping
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            AllowNullDestinationValues = true;
            CreateMap<RegisterApiModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
            CreateMap<User, UserResponseModel>().ReverseMap();
            CreateMap<UserRequestModel, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
