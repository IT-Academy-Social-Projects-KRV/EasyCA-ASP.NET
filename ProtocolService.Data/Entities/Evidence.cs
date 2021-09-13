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
    public class Evidence
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EvidenceId { get; set; }

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
