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

        [BsonElement("Cross Street")]
        [JsonProperty("Cross Street")]
        public string CrossStreet { get; set; }

        [BsonElement("Latityde")]
        [JsonProperty("Latityde")]
        public string CoordinatesOfLatitude { get; set; }

        [BsonElement("Longitude")]
        [JsonProperty("Longitude")]
        public string CoordinatesOfLongitude { get; set; }

        [BsonElement("Is in City")]
        [JsonProperty("Is in City")]
        public bool IsInCity { get; set; }

        [BsonElement("Is Intersection")]
        [JsonProperty("Is Intersection")]
        public bool IsIntersection { get; set; }
    }
}
