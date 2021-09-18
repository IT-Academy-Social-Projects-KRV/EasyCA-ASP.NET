using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace AccountService.Data.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task<UpdateResult> UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> entity);
        Task<DeleteResult> DeleteAsync(FilterDefinition<TEntity> filter);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsyncByFilter(FilterDefinition<TEntity> filter);
        Task<TEntity> GetByFilterAsync(FilterDefinition<TEntity> filter);
    }
}
