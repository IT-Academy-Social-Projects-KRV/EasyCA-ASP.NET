using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CrudMicroservice.Data.Entities
{
    public class EvidenceCA
    {
        [BsonElement("PhotoSchema")]
        [JsonProperty("PhotoSchema")]
        public string PhotoSchema { get; set; }        
    }
}
