using System.Collections.Generic;
using ProtocolService.Data.Entities;

namespace ProtocolService.Domain.ApiModel.RequestApiModels
{
    public class SideRequestModel
    {
        public string Email { get; set; }
        public string TransportId { get; set; }
        public List<int> Circumstances { get; set; }
        public List<Evidence> Evidences { get; set; }
        public string DriverLicenseSerial { get; set; }
        public string Damage { get; set; }
        public bool IsGulty { get; set; }
    }
}