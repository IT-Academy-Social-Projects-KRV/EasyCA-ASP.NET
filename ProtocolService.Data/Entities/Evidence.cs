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

        [BsonElement("Photo/schema")]
        [BsonRepresentation(BsonType.Undefined)]
        [JsonProperty("Photo/schema")]
        public object PhotoSchema { get; set; }

        [BsonElement("Attachments")]
        [BsonRepresentation(BsonType.Undefined)]
        [JsonProperty("Attachments")]
        public List<object> Attachments { get; set; }
    }
}
