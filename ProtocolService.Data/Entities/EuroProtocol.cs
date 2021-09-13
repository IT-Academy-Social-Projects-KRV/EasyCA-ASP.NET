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
    public class EuroProtocol
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Date Time")]
        [JsonProperty("Date Time")]
        public DateTime DateTimeGroup { get; set; }

        [BsonElement("Address Of Accident")]
        [JsonProperty("Address Of Accident")]
        public AddressOfAccident Address { get; set; } //уточнити

        [BsonElement("Side A")]
        [JsonProperty("Side A")]
        public string SideAId { get; set; }

        [BsonElement("Side B")]
        [JsonProperty("Side B")]
        public string SideBId { get; set; }

        [BsonElement("List of Witnesses")]
        [JsonProperty("List of Witnesses")]
        public List<Witness> Witnesses { get; set; }
    }
}
