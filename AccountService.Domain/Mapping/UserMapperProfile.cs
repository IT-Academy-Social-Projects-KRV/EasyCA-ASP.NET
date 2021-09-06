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
            CreateMap<User, UserRequestModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
        }
    }
}
