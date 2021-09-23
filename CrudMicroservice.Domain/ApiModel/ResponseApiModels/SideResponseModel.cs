namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class SideResponseModel
    {
        public string Email { get; set; }
        public string TransportId { get; set; }
        public System.Collections.Generic.IEnumerable<int> Circumstances { get; set; }
        public System.Collections.Generic.IEnumerable<EvidenceResponseModel> Evidences { get; set; }
        public string DriverLicenseSerial { get; set; }
        public string Damage { get; set; }
        public bool IsGulty { get; set; }
    }
}