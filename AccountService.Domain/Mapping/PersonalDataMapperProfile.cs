using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AutoMapper;

namespace AccountService.Domain.Mapping
{
    public class PersonalDataMapperProfile : Profile
    {
        public PersonalDataMapperProfile()
        {
            AllowNullCollections = true;
            CreateMap<PersonalData, PersonalDataResponseModel>()
                .ForMember(dest=>dest.Address, x=>x.MapFrom(opt=>opt.UserAddress)).ReverseMap(); 
            CreateMap<PersonalData, PersonalDataRequestModel>().ReverseMap();
        }
    }
}
