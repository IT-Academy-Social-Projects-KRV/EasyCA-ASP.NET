using ProtocolService.Data.Entities;
using System;
using System.Collections.Generic;

namespace ProtocolService.Domain.ApiModel.ResponceApiModels
{
    public class EuroProtocolResponseModel
    {
        public DateTime RegistrationDateTime { get; set; }
        public AddressOfAccident Address { get; set; }
        public Side SideA { get; set; }
        public Side SideB { get; set; }
        public bool IsClosed { get; set; }
        public List<Witness> Witnesses { get; set; }
    }
}
