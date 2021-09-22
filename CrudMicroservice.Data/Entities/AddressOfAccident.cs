using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CrudMicroservice.Data.Entities
{
    public class AddressOfAccident
    {        
        [BsonElement("City")]
        [JsonProperty("City")]
        public string City { get; set; }

        [BsonElement("District")]
        [JsonProperty("District")]
        public string District { get; set; }

        [BsonElement("Street")]
        [JsonProperty("Street")]
        public string Street { get; set; }

        [BsonElement("CrossStreet")]
        [JsonProperty("CrossStreet")]
        public string CrossStreet { get; set; }

        [BsonElement("Latitude")]
        [JsonProperty("Latitude")]
        public string CoordinatesOfLatitude { get; set; }

        [BsonElement("Longitude")]
        [JsonProperty("Longitude")]
        public string CoordinatesOfLongitude { get; set; }

        [BsonElement("IsInCity")]
        [JsonProperty("IsInCity")]
        public bool IsInCity { get; set; }

        [BsonElement("IsIntersection")]
        [JsonProperty("IsIntersection")]
        public bool IsIntersection { get; set; }
    }
}
