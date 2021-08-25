using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
#region snippet_NewtonsoftJsonImport
using Newtonsoft.Json;
#endregion

namespace AccountService.Data.Entities
{
    public class Transport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        #region snippet_UserIdProperty
        [BsonElement ("UserId")]
        [JsonProperty("UserId")]
        #endregion
        public int UserId { get; set; }
        [BsonElement("MadeBy")]
        [JsonProperty("MadeBy")]
        public string ProducedBy { get; set; }
        [BsonElement("Model")]
        [JsonProperty("Model")]
        public string Model { get; set; }
        [BsonElement("CategoryId")]
        [JsonProperty("CategoryId")]
        public Category CarCategory { get; set; }
        [BsonElement("VinCode")]
        [JsonProperty("VinCode")]
        public string VINCode { get; set; }
        [BsonElement("CarPlate")]
        [JsonProperty("CarPlate")]
        public string CarPlate { get; set; }
        [BsonElement("Color")]
        [JsonProperty("Color")]
        public string Color { get; set; }
        [BsonElement("Year of production")]
        [JsonProperty("Year of production")]
        public int YearofProduction { get; set; }
        [BsonElement("Insuarance serial number")]
        [JsonProperty("Insuarance serial number")]
        public string InsuaranceSerialNumber { get; set; }
    }
}