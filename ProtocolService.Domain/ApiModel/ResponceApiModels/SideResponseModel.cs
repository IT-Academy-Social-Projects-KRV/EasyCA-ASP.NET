using System.Collections.Generic;

namespace ProtocolService.Domain.ApiModel.ResponceApiModels
{
    public class SideResponseModel
    {
        public string Email { get; set; }
        public string TransportId { get; set; }
        public IEnumerable<int> Circumstances { get; set; }
        public IEnumerable<EvidenceResponseModel> Evidences { get; set; }
        public string DriverLicenseSerial { get; set; }
        public string Damage { get; set; }
        public bool IsGulty { get; set; }
    }
}
