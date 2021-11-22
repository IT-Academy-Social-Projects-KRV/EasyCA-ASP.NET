using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CrudMicroservice.Data.Entities
{
    public class Evidence
    {
        [BsonElement("PhotoSchema")]
        [JsonProperty("PhotoSchema")]
        public string PhotoSchema { get; set; }
    }
}
