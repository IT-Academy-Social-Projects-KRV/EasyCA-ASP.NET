using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
#region snippet_NewtonsoftJsonImport
using Newtonsoft.Json;
#endregion

namespace AccountService.Data.Entities
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Country")]
        [JsonProperty("Country")]
        public string Country { get; set; }
        [BsonElement("Region")]
        [JsonProperty("Region")]
        public string Region { get; set; }
        [BsonElement("City")]
        [JsonProperty("City")]
        public string City { get; set; }
        [BsonElement("District")]
        [JsonProperty("District")]
        public string District { get; set; }
        [BsonElement("Street")]
        [JsonProperty("Street")]
        public string Street { get; set; }
        [BsonElement("BuildingNumber")]
        [JsonProperty("BuildingNumber")]
        public string Building { get; set; }
        [BsonElement("AppartamentNumber")]
        [JsonProperty("AppartametNumber")]
        public int Appartament { get; set; }
        [BsonElement("PostalCode")]
        [JsonProperty("PostalCode")]
        public string PostalCode { get; set; }

    }
}
