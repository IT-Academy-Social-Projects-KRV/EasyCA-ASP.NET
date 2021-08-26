using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
#region snippet_NewtonsoftJsonImport
using Newtonsoft.Json;
#endregion

namespace AccountService.Data.Entities
{
    [CollectionName("transportCategories")]
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
