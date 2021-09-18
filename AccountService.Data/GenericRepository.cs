using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountService.Data.Interfaces;
using MongoDB.Driver;

namespace AccountService.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dBContext;
        private readonly IMongoCollection<TEntity> _dbCollection;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dBContext = dbContext;
            _dbCollection = _dBContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbCollection.InsertOneAsync(entity);
        }

        public async Task<UpdateResult> UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> entity)
        {
            return await _dbCollection.UpdateOneAsync(filter, entity);
        }

        public async Task<DeleteResult> DeleteAsync(FilterDefinition<TEntity> filter)
        {
            return await _dbCollection.DeleteOneAsync(filter);
        }

        public async Task<TEntity> GetByFilterAsync(FilterDefinition<TEntity> filter)
        {
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbCollection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsyncByFilter(FilterDefinition<TEntity> filter)
        {
            return await _dbCollection.Find(filter).ToListAsync();
        }
    }
}
