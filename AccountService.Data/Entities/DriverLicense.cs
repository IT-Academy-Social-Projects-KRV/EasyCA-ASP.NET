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
        [BsonElement("LicenseSerialNumber")]
        [JsonProperty("LicenseSerialNumber")]
        public string LicenseSerialNumber { get; set; }
        [BsonElement("IssuedAuthority")]
        [JsonProperty("IssuedAuthority")]
        public string IssuedBy { get; set; }
        [BsonElement("ExpirationDate")]
        [JsonProperty("ExpirationDate")]
        public BsonDateTime ExpirationDate { get; set; }
        [BsonElement("OpenedCategories")]
        [JsonProperty("OpenedCategories")]
        public List<TransportCategory> UserCategories { get; set; }
    }
}