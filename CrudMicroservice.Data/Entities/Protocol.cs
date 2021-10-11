﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudMicroservice.Data.Entities
{
    [CollectionName("Protocol")]
    public class Protocol
    {
        public Protocol()
        {
            SerialNumber = ObjectId.GenerateNewId().ToString();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("SerialNumber")]
        [JsonProperty("SerialNumber")]
        public string SerialNumber { get; set; }

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

        [BsonElement("IsClosed")]
        [JsonProperty("IsClosed")]
        public bool IsClosed { get; set; }

        [BsonElement("ListOfWitnesses")]
        [JsonProperty("ListOfWitnesses")]
        public List<Witness> Witnesses { get; set; }

    }
}
