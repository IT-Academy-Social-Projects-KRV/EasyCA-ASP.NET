﻿using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Mapping
{
    public class SideMapperProfile : Profile
    {
        public SideMapperProfile()
        {
            CreateMap<Side, SideRequestModel>().ReverseMap();
            CreateMap<Side, SideResponseModel>().ReverseMap();
        }
    }
}
