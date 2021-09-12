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

        //DTG

        public AddressOfAccident Address { get; set; } //уточнити

        [BsonElement("Side A Id")]
        [JsonProperty("Side A Id")]
        public string SideAId { get; set; }

        [BsonElement("Side B Id")]
        [JsonProperty("Side B Id")]
        public string SideBId { get; set; }

        [BsonElement("List of Witnesses")]
        [JsonProperty("List of Witnesses")]
        public List<Witness> Witnesses { get; set; }
    }
}
