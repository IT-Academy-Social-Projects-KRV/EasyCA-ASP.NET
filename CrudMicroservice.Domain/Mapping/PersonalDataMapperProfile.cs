﻿using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using AutoMapper;

namespace CrudMicroservice.Domain.Mapping
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