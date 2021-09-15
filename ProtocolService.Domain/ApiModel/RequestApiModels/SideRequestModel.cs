using System.Collections.Generic;
using ProtocolService.Data.Entities;

namespace ProtocolService.Domain.ApiModel.RequestApiModels
{
    public class SideRequestModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string TransportId { get; set; }
        public List<string> Circumstances { get; set; }
        public List<Evidence> Evidences { get; set; }
        public string DriverLicenseSerial { get; set; }
        public string Damage { get; set; }
        public bool IsGulty { get; set; }
    }
}