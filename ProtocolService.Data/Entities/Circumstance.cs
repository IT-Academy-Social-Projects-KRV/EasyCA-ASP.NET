using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ProtocolService.Data.Entities
{
    public class Circumstance
    {
        [BsonId]
        public int CircumstanceId { get; set; }

        [BsonElement("CircumstanceName")]
        [JsonProperty("CircumstanceName")]
        public string CircumstanceName { get; set; }
    }
}
