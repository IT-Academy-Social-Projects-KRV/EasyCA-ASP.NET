using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class SideResponseApiModel
    {
        public string Email { get; set; }
        public string TransportId { get; set; }
        public IEnumerable<int> Circumstances { get; set; }
        public IEnumerable<EvidenceResponseApiModel> Evidences { get; set; }
        public string DriverLicenseSerial { get; set; }
        public string Damage { get; set; }
        public bool IsGulty { get; set; }
    }
}