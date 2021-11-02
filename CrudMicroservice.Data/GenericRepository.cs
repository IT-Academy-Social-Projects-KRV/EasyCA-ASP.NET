using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrudMicroservice.Data.Interfaces;
using MongoDB.Driver;

namespace CrudMicroservice.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly CrudDbContext _dbContext;
        private readonly IMongoCollection<TEntity> _dbCollection;

        public GenericRepository(CrudDbContext dbContext)
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

        public async Task<List<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbCollection.Find(predicate).ToListAsync();
        }

        public async Task<TEntity> GetLastItem(Predicate<TEntity> predicate)
        {
            var list = await _dbCollection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();

            return list.FindLast(predicate);
        }
    }
}
