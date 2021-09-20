using ProtocolService.Data.Entities;
using System;
using System.Collections.Generic;

namespace ProtocolService.Domain.ApiModel.RequestApiModels
{
    public class EuroProtocolRequestModel
    {
        public EuroProtocolRequestModel()
        {
            RegistrationDateTime = DateTime.Now;
            IsClosed = false;
        }

        public DateTime RegistrationDateTime { get; set; }
        public AddressOfAccidentRequestModel Address { get; set; }
        public SideRequestModel SideA { get; set; }
        public SideRequestModel SideB { get; set; }
        public bool IsClosed { get; set; }
        public List<Witness> Witnesses { get; set; }       
    }
}
