using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.RequestApiModels;
using AutoMapper;

namespace AccountService.Domain.Mapping
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            AllowNullDestinationValues = true;
            CreateMap<RegisterApiModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserData, opt => opt.MapFrom(src=>new PersonalData()));              
        }
    }
}
