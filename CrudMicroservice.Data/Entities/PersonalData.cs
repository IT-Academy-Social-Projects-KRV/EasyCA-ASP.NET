using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using MongoDbGenericRepository.Attributes;

namespace CrudMicroservice.Data.Entities
{
    [CollectionName("PersonalDatas")]
    public class PersonalData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserAddress")]
        [JsonProperty("UserAddress")]
        public Address Address { get; set; }
        [BsonElement("UserIpn")]
        [JsonProperty("UserIpn")]
        public string IPN { get; set; }
        [BsonElement("InspectorId")]
        [JsonProperty("InspectorId")]
        public string ServiceNumber { get; set; }
        [BsonElement("UserBirthDay")]
        [JsonProperty("UserBirthDay")]
        public DateTime BirthDay { get; set; }
        [BsonElement("UserJob")]
        [JsonProperty("UserJob")]
        public string JobPosition { get; set; }
        [BsonElement("UserDriverLicense")]
        [JsonProperty("UserDriverLicense")]
        public DriverLicense UserDriverLicense { get; set; }
        [BsonElement("UserCars")]
        [JsonProperty("UserCars")]
        public List<string> UserCars { get; set; }
    }
}
