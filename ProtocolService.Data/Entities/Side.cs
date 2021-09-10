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

        [BsonElement("Transport Id")]
        [JsonProperty("Transport Id")]
        public string TransportId { get; set; }

        [BsonElement("List of circumstances")]
        [JsonProperty("List of circumstances")]
        public List<Circumstances> Circumstances { get; set; }

        [BsonElement("List of evidences")]
        [JsonProperty("List of evidences")]
        public List<Evidence> Evidences { get; set; }

        [BsonElement("DriverLicense")]
        [JsonProperty("DriverLicense")]
        public string DriverLicenseSerial { get; set; }

        [BsonElement("Damage")]
        [JsonProperty("Damage")]
        public string Damage { get; set; }

        [BsonElement("IsBulty")]
        [JsonProperty("IsBulty")]
        public bool IsGulty { get; set; }
    }
}
