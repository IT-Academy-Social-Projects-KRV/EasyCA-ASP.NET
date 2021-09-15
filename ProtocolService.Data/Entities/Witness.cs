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

        [BsonElement("WitnessFirstName")]
        [JsonProperty("WitnessFirstName")]
        public string FirstName { get; set; }

        [BsonElement("WitnessLastName")]
        [JsonProperty("WitnessLastName")]
        public string LastName { get; set; }

        [BsonElement("WitnessAddress")]
        [JsonProperty("WitnessAddress")]        
        public string WitnessAddress { get; set; } 

        [BsonElement("WitnessPhoneNumber")]
        [JsonProperty("WitnessPhoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("IsVictim")]
        [JsonProperty("IsVictim")]
        public bool IsVictim { get; set; }
    }
}
