using System;
using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class EuroProtocolResponseApiModel
    {
        public string Id { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string SerialNumber { get; set; }
        public AddressOfAccidentResponseApiModel Address { get; set; }
        public SideResponseApiModel SideA { get; set; }
        public SideResponseApiModel SideB { get; set; }
        public bool IsClosed { get; set; }
        public IEnumerable<WitnessResponseApiModel> Witnesses { get; set; }
    }
}
