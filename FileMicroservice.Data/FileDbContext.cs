using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileMicroservice.Data
{
    public class FileDbContext
    {
        private readonly IMongoDatabase _dbContext;

        public FileDbContext(IMongoClient client, string dbName)
        {
            _dbContext = client.GetDatabase(dbName);
        }

        public GridFSBucket GetFileBucket()
        {
            return new GridFSBucket(_dbContext);
        }
    }
}
