using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CrudMicroservice.Data.Entities
{
    public class Side
    {
        [BsonElement("Email")]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [BsonElement("TransportId")]
        [JsonProperty("TransportId")]
        public string TransportId { get; set; }

        [BsonElement("ListOfCircumstances")]
        [JsonProperty("ListOfCircumstances")]
        public List<int> Circumstances { get; set; }

        [BsonElement("ListOfEvidences")]
        [JsonProperty("ListOfEvidences")]
        public List<Evidence> Evidences { get; set; }

        [BsonElement("DriverLicense")]
        [JsonProperty("DriverLicense")]
        public string DriverLicenseSerial { get; set; }

        [BsonElement("Damage")]
        [JsonProperty("Damage")]
        public string Damage { get; set; }

        [BsonElement("IsGulty")]
        [JsonProperty("IsGulty")]
        public bool IsGulty { get; set; }
    }
}
