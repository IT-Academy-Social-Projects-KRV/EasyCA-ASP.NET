using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using Newtonsoft.Json;

namespace AccountService.Data.Entities
{
    [CollectionName("Transports")]
    public class Transport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement ("UserId")]
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [BsonElement("MadeBy")]
        [JsonProperty("MadeBy")]
        public string ProducedBy { get; set; }
        [BsonElement("Model")]
        [JsonProperty("Model")]
        public string Model { get; set; }
        [BsonElement("CategoryId")]
        [JsonProperty("CategoryId")]
        public TransportCategory CarCategory { get; set; }
        [BsonElement("VinCode")]
        [JsonProperty("VinCode")]
        public string VINCode { get; set; }
        [BsonElement("CarPlate")]
        [JsonProperty("CarPlate")]
        public string CarPlate { get; set; }
        [BsonElement("Color")]
        [JsonProperty("Color")]
        public string Color { get; set; }
        [BsonElement("YearOfProduction")]
        [JsonProperty("YearOfProduction")]
        public int YearOfProduction { get; set; }
        [BsonElement("InsuaranceSerialNumber")]
        [JsonProperty("InsuaranceSerialNumber")]
        public Insuarance InsuaranceNumber { get; set; }
    }
}