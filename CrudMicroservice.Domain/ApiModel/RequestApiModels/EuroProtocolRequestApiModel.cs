using System;
using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class EuroProtocolRequestApiModel
    {
        public EuroProtocolRequestApiModel()
        {
            RegistrationDateTime = DateTime.Now;
            IsClosed = false;
        }

        public DateTime RegistrationDateTime { get; set; }
        public string SerialNumber { get; set; }
        public AddressOfAccidentRequestApiModel Address { get; set; }
        public SideRequestApiModel SideA { get; set; }
        public SideRequestApiModel SideB { get; set; }
        public bool IsClosed { get; set; }
        public IEnumerable<WitnessRequestApiModel> Witnesses { get; set; }
    }
}
