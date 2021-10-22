using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CrudMicroservice.Data.Entities
{
    public class SideCA
    {
        [BsonElement("Email")]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [BsonElement("TransportId")]
        [JsonProperty("TransportId")]
        public string TransportId { get; set; }

        [BsonElement("DriverLicense")]
        [JsonProperty("DriverLicense")]
        public string DriverLicenseSerial { get; set; }
    }
}
