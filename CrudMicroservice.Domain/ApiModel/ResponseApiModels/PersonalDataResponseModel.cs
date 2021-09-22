using System;
using System.Collections.Generic;
using CrudMicroservice.Data.Entities;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class PersonalDataResponseModel
    {
        public Address Address { get; set; }
        public string IPN { get; set; }
        public string ServiceNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public string JobPosition { get; set; }
        public DriverLicense UserDriverLicense { get; set; }
        public List<string> UserCars { get; set; }
    }
}
