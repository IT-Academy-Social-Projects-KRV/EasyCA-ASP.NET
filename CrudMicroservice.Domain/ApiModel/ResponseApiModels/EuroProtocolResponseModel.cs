namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class EuroProtocolResponseModel
    {
        public string Id { get; set; }
        public System.DateTime RegistrationDateTime { get; set; }
        public string SerialNumber { get; set; }
        public AddressOfAccidentResponseModel Address { get; set; }
        public SideResponseModel SideA { get; set; }
        public SideResponseModel SideB { get; set; }
        public bool IsClosed { get; set; }
        public System.Collections.Generic.IEnumerable<WitnessResponseModel> Witnesses { get; set; }
    }
}
