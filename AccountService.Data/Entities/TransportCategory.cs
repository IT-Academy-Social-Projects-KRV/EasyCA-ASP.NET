using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using Newtonsoft.Json;

namespace AccountService.Data.Entities
{
    [CollectionName("TransportCategories")]
    public class TransportCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("CategoryName")]
        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }
    }
}
