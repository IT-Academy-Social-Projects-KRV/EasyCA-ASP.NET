using MongoDB.Driver;

namespace AccountService.WebApi.Services
{
    public interface IMongoDbService
    {
        public MongoClient Client { get; }
    }
}
