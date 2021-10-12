using System.Collections.Generic;
using CrudMicroservice.Data.Entities;

namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class SideRequestApiModel
    {
        public string Email { get; set; }
        public string TransportId { get; set; }
        public List<int> Circumstances { get; set; }
        public List<Evidence> Evidences { get; set; }
        public string DriverLicenseSerial { get; set; }
        public string Damage { get; set; }
        public bool IsGulty { get; set; }
        public string ProtocolSerial { get; set; }
    }
}