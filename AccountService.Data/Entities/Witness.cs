using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
#region snippet_NewtonsoftJsonImport
using Newtonsoft.Json;
#endregion

namespace AccountService.Data.Entities
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
        public Address WitnessAddress { get; set; }
        [BsonElement("Witness Phone Number")]
        [JsonProperty("Witness Phone Number")]
        public string PhoneNumber { get; set; }
        [BsonElement("Is victim")]
        [JsonProperty("Is victim")]
        public bool IsVctim { get; set; }
    }
}
