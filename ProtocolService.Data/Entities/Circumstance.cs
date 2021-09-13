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
    public class Circumstance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CircumstanceId { get; set; }

        [BsonElement("CircumstanceName")]
        [JsonProperty("CircumstanceName")]
        public string CircumstanceName { get; set; }
    }
}
