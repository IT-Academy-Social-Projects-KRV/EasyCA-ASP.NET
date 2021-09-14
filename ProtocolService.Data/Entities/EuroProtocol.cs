using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ProtocolService.Data.Entities
{
    public class EuroProtocol
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("RegistrationDateTime")]
        [JsonProperty("RegistrationDateTime")]
        public DateTime RegistrationDateTime { get; set; }

        [BsonElement("AddressOfAccident")]
        [JsonProperty("AddressOfAccident")]
        public AddressOfAccident Address { get; set; } 

        [BsonElement("SideA")]
        [JsonProperty("SideA")]
        public Side SideA { get; set; }

        [BsonElement("SideB")]
        [JsonProperty("SideB")]
        public Side SideB { get; set; }

        [BsonElement("ListOfWitnesses")]
        [JsonProperty("ListOfWitnesses")]
        public List<Witness> Witnesses { get; set; }
    }
}
