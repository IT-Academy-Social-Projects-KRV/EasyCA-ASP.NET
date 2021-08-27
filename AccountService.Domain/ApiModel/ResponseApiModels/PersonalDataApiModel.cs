using System;
using System.Collections.Generic;
using AccountService.Data.Entities;

namespace AccountService.Domain.ApiModel.ResponseApiModels
{
    public class PersonalDataApiModel
    {
        public Address Address { get; set; }
        public string IPN { get; set; }
        public string ServiceNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public string JobPosition { get; set; }
        public DriverLicense UserDriverLicense { get; set; }
        public List<Transport> UserCars { get; set; }
    }
}
