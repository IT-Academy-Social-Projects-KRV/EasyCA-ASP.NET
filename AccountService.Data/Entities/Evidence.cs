using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
#region snippet_NewtonsoftJsonImport
using Newtonsoft.Json;
using System.Collections.Generic;
#endregion

namespace AccountService.Data.Entities
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
