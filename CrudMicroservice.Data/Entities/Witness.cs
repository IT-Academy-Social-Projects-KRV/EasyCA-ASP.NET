﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CrudMicroservice.Data.Entities
{
    public class Witness
    {
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
