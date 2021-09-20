using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using Newtonsoft.Json;

namespace ProtocolService.Data.Entities
{
    [CollectionName("Circumstances")]
    public class Circumstance
    {
        [BsonId]
        public int CircumstanceId { get; set; }

        [BsonElement("CircumstanceName")]
        [JsonProperty("CircumstanceName")]
        public string CircumstanceName { get; set; }
    }
}
