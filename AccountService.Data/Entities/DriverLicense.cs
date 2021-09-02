using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public DateTime ExpirationDate { get; set; }
        [BsonElement("OpenedCategories")]
        [JsonProperty("OpenedCategories")]
        public List<string> UserCategories { get; set; }
    }
}