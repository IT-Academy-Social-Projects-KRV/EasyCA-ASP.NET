﻿using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Mapping
{
    public class CarAccidentMappingProfile:Profile
    {
        public CarAccidentMappingProfile()
        {
            CreateMap<CarAccident, CarAccidentRequestApiModel>().ReverseMap();
            CreateMap<CarAccident, CarAccidentResponseApiModel>().ReverseMap();
        }
    }
}
