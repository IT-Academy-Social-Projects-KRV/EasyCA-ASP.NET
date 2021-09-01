using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AutoMapper;

namespace AccountService.Domain.Mapping
{
    public class PersonalDataMapperProfile : Profile
    {
        public PersonalDataMapperProfile()
        {
            AllowNullCollections = true;
            CreateMap<PersonalData, PersonalDataResponseModel>().ReverseMap();
        }
    }
}