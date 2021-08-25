using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
#region snippet_NewtonsoftJsonImport
using Newtonsoft.Json;
using System.Collections.Generic;
#endregion

namespace AccountService.Data.Entities
{
    public class Side
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonElement("Transport Id")]
        [JsonProperty("Transport Id")]
        public string TransportId { get; set; }
        [BsonElement("List of circumstances")]
        [JsonProperty("List of circumstances")]
        public List<Circumstances> Circumstances { get; set; }
        [BsonElement("List of evidences")]
        [JsonProperty("List of evidences")]
        public List<Evidence> Evidences { get; set; }
        [BsonElement("DriverLicense")]
        [JsonProperty("DriverLicense")]
        public string DriverLicenseSerial { get; set; }
        [BsonElement("Damages")]
        [JsonProperty("Damages")]
        public string Damage { get; set; }
        [BsonElement("IsGuilty")]
        [JsonProperty("IsGuilty")]
        public bool IsGuilty { get; set; }
    }
}
