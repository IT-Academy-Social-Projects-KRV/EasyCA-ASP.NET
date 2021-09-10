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
    public class Witness
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Witness First Name")]
        [JsonProperty("Witness First Name")]
        public string FirstName { get; set; }

        [BsonElement("Witness Last Name")]
        [JsonProperty("Witness Last Name")]
        public string LastName { get; set; }

        [BsonElement("Witness Address")]
        [JsonProperty("Witness Address")]
        //чи просто стрінгою передавати
        public Address WitnessAddress { get; set; } 

        [BsonElement("Witness Phone Number")]
        [JsonProperty("Witness Phone Number")]
        public string PhoneNumber { get; set; }

        [BsonElement("Is Victim")]
        [JsonProperty("Is Victim")]
        public bool IsVictim { get; set; }
    }
}
