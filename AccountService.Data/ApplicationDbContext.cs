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

        public IMongoCollection<DriverLicense> DriverLicenses => _dbContext.GetCollection<DriverLicense>("driverLicenses");
    }
}
