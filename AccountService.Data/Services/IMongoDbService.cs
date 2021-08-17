using MongoDB.Driver;

namespace AccountService.Data.Services
{
    public interface IMongoDbService
    {
        public MongoClient Client { get; }
    }
}
