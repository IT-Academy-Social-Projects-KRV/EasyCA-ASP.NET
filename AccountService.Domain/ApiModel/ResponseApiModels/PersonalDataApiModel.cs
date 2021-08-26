using System;
using AccountService.Data.Entities;

namespace AccountService.Domain.ApiModel.ResponseApiModels
{
    public class PersonalDataApiModel
    {
        public string Address { get; set; }
        public string IPN { get; set; }
        public string ServiceId { get; set; }
        public DateTime BirthDay { get; set; }
        public string JobPosition { get; set; }
        public string Citizen { get; set; }
        public DriverLicense UserDriverLicense { get; set; }
    }
}
