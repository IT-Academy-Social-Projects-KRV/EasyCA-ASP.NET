using MongoDB.Driver;
using ProtocolService.Data.Entities;

namespace ProtocolService.Data
{
    public class ProtocolDbContext
    {
        private readonly IMongoDatabase _dbContext;
        public ProtocolDbContext(IMongoClient client, string dbName)
        {
            _dbContext = client.GetDatabase(dbName);
        }
        public IMongoCollection<Circumstance> Circumstances => _dbContext.GetCollection<Circumstance>("Circumstances");
        public IMongoCollection<EuroProtocol> EuroProtocols => _dbContext.GetCollection<EuroProtocol>("EuroProtocols");
    }
}
