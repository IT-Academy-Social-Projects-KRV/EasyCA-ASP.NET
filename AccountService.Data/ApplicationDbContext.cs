using AccountService.Data.Entities;
using MongoDB.Driver;

namespace AccountService.Data
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _dbContext;

        public ApplicationDbContext (IMongoClient client, string dbName)
        {
            _dbContext = client.GetDatabase(dbName);
        }

        public IMongoCollection<Transport> Transports => _dbContext.GetCollection<Transport>("transports");
        public IMongoCollection<TransportCategory> TransportCategories => _dbContext.GetCollection<TransportCategory>("transportCategories");
    }
}
