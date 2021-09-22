using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CrudMicroservice.Data.Entities
{
    public class Evidence
    {
        [BsonElement("Explanation")]
        [JsonProperty("Explanation")]
        public string Explanation { get; set; }

        [BsonElement("PhotoSchema")]
        [JsonProperty("PhotoSchema")]
        public string PhotoSchema { get; set; }

        [BsonElement("Attachments")]
        [JsonProperty("Attachments")]
        public List<string> Attachments { get; set; }
    }
}
