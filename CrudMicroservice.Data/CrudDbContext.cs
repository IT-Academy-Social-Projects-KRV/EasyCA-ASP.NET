using CrudMicroservice.Data.Entities;
using MongoDB.Driver;

namespace CrudMicroservice.Data
{
    public class CrudDbContext
    {
        private readonly IMongoDatabase _dbContext;

        public CrudDbContext(IMongoClient client, string dbName)
        {
            _dbContext = client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _dbContext.GetCollection<T>(name);
        }

        public IMongoCollection<PersonalData> PersonalDatas => _dbContext.GetCollection<PersonalData>("PersonalDatas");
        public IMongoCollection<TransportCategory> TransportCategories => _dbContext.GetCollection<TransportCategory>("TransportCategories");
        public IMongoCollection<Circumstance> Circumstances => _dbContext.GetCollection<Circumstance>("Circumstances");
        public IMongoCollection<EuroProtocol> EuroProtocols => _dbContext.GetCollection<EuroProtocol>("EuroProtocols");
        public IMongoCollection<CarAccident> CarAccidents => _dbContext.GetCollection<CarAccident>("CarAccidents");
    }
}
