using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
#region snippet_NewtonsoftJsonImport
using Newtonsoft.Json;
#endregion

namespace AccountService.Data.Entities
{
    public class Insuarance
    {
        [BsonElement("Company Name")]
        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }
        [BsonElement("SerialNumber")]
        [JsonProperty("SerialNumber")]
        public string SerialNumber { get; set; }
    }
}
