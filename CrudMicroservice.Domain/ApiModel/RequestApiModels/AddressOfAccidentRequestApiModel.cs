namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class AddressOfAccidentRequestApiModel
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string CrossStreet { get; set; }
        public string CoordinatesOfLatitude { get; set; }
        public string CoordinatesOfLongitude { get; set; }
        public bool IsInCity { get; set; }
        public bool IsIntersection { get; set; }
    }
}
