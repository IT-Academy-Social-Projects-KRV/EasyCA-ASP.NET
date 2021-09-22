﻿using AuthMicroservice.Data.Entities;
using AuthMicroservice.Domain.ApiModel.RequestApiModels;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;
using AutoMapper;

namespace AuthMicroservice.Domain.Mapping
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            AllowNullDestinationValues = true;
            CreateMap<RegisterApiModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
        }
    }
}
