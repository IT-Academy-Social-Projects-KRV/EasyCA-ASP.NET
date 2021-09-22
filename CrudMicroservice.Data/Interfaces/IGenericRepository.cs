using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CrudMicroservice.Data.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task<UpdateResult> UpdateAsync(Expression<Func<TEntity, bool>> predicate, UpdateDefinition<TEntity> entity);
        Task<ReplaceOneResult> ReplaceAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity);
        Task<DeleteResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
