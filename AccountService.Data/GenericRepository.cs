using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AccountService.Data.Interfaces;
using MongoDB.Driver;

namespace AccountService.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMongoCollection<TEntity> _dbCollection;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            var collectionName = typeof(TEntity).CustomAttributes.FirstOrDefault().ConstructorArguments.FirstOrDefault().Value.ToString();

            if (!String.IsNullOrEmpty(collectionName))
            {
                _dbCollection = _dbContext.GetCollection<TEntity>(collectionName);
            }
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbCollection.InsertOneAsync(entity);
        }

        public async Task<UpdateResult> UpdateAsync(Expression<Func<TEntity, bool>> predicate, UpdateDefinition<TEntity> entity)
        {
            return await _dbCollection.UpdateOneAsync(predicate, entity);
        }

        public async Task<ReplaceOneResult> ReplaceAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            return await _dbCollection.ReplaceOneAsync(predicate, entity);
        }

        public async Task<DeleteResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbCollection.DeleteOneAsync(predicate);
        }

        public async Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _dbCollection.FindAsync(predicate);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbCollection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbCollection.Find(predicate).ToListAsync();
        }
    }
}
