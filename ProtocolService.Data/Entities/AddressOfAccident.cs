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
    public class AddressOfAccident
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EuroProtocolId { get; set; }

        [BsonElement("City")]
        [JsonProperty("City")]
        public string City { get; set; }

        [BsonElement("District")]
        [JsonProperty("District")]
        public string District { get; set; }

        [BsonElement("Street")]
        [JsonProperty("Street")]
        public string Street { get; set; }

        [BsonElement("Latityde")]
        [JsonProperty("Latityde")]
        public string CoordinatesOfLatitude { get; set; }

        [BsonElement("Longitude")]
        [JsonProperty("Longitude")]
        public string CoordinatesOfLongitude { get; set; }
    }
}
