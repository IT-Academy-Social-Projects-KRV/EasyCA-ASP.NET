using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolService.Data.Entities
{
    public class Side
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("TransportId")]
        [JsonProperty("TransportId")]
        public string TransportId { get; set; }

        [BsonElement("ListOfCircumstances")]
        [JsonProperty("ListOfCircumstances")]
        public List<Circumstance> Circumstances { get; set; }

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
