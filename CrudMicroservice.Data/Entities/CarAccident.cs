using MongoDbGenericRepository.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CrudMicroservice.Data.Entities
{
    [CollectionName("CarAccidents")]
    public class CarAccident
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("SerialNumber")]
        [JsonProperty("SerialNumber")]
        public string SerialNumber { get; set; }

        [BsonElement("InspectorId")]
        [JsonProperty("InspectorId")]
        public string InspectorId { get; set; }

        [BsonElement("RegistrationDateTime")]
        [JsonProperty("RegistrationDateTime")]
        public DateTime RegistrationDateTime { get; set; }

        [BsonElement("AddressOfAccident")]
        [JsonProperty("AddressOfAccident")]
        public AddressOfAccident Address { get; set; }

        [BsonElement("SideOfAccident")]
        [JsonProperty("SideOfAccident")]
        public SideCA SideOfAccident { get; set; }

        [BsonElement("AccidentCircumstances")]
        [JsonProperty("AccidentCircumstances")]
        public string AccidentCircumstances { get; set; }

        [BsonElement("TrafficRuleId")]
        [JsonProperty("TrafficRuleId")]
        public string TrafficRuleId { get; set; }

        [BsonElement("DriverExplanation")]
        [JsonProperty("DriverExplanation")]
        public string DriverExplanation { get; set; }

        [BsonElement("ListOfWitnesses")]
        [JsonProperty("ListOfWitnesses")]
        public List<Witness> Witnesses { get; set; }

        [BsonElement("ListOfEvidences")]
        [JsonProperty("ListOfEvidences")]
        public List<Evidence> Evidences { get; set; }

        [BsonElement("CourtDTG")]
        [JsonProperty("CourtDTG")]
        public DateTime CourtDTG { get; set; }

        [BsonElement("IsDocumentTakenOff")]
        [JsonProperty("IsDocumentTakenOff")]
        public bool IsDocumentTakenOff { get; set; }
    }
}
