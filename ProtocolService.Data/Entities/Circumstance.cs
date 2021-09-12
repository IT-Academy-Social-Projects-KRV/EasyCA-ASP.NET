using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ProtocolService.Data.Entities
{
    public class Circumstance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CircumstanceId { get; set; }
        [BsonElement("Circumstances Name")]
        [JsonProperty("Circumstances Name")]
        public string CircumstanceName { get; set; }
    }
}
