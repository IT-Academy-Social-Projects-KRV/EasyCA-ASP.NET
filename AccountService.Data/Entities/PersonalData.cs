using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AccountService.Data.Entities
{
    public class PersonalData
    {
        [BsonElement("UserAddress")]
        [JsonProperty("UserAddress")]
        public Address UserAddress { get; set; }
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
        public List<Transport> UserCars { get; set; }
    }
}
