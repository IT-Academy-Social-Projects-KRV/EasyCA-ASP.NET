using System;
using CrudMicroservice.Data.Entities;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class EuroProtocolSimpleResponseApiModel
    {
        public string SerialNumber { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public AddressOfAccident Address { get; set; }
        public bool IsClosed { get; set; }
    }
}
