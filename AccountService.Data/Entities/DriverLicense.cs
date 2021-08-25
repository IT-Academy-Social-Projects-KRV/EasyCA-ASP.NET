using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
#region snippet_NewtonsoftJsonImport
using Newtonsoft.Json;
using System.Collections.Generic;
#endregion
namespace AccountService.Data.Entities
{
    public class DriverLicense
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserId")]
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [BsonElement("License Serial Number")]
        [JsonProperty("License Serial Number")]
        public string LicenseSerialNumber { get; set; }
        [BsonElement("Issued Authority")]
        [JsonProperty("IssuedAuthority")]
        public string IssuedBy { get; set; }
        [BsonElement("ExpirationDate")]
        [JsonProperty("ExpirationDate")]
        public BsonDateTime ExpirationDate { get; set; }

    }
}
