using System;
using System.Collections.Generic;

namespace ProtocolService.Domain.ApiModel.ResponceApiModels
{
    public class EuroProtocolResponseModel
    {
        public string Id{ get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public AddressOfAccidentResponseModel Address { get; set; }
        public SideResponseModel SideA { get; set; }
        public SideResponseModel SideB { get; set; }
        public bool IsClosed { get; set; }
        public IEnumerable<WitnessResponseModel> Witnesses { get; set; }
    }
}
