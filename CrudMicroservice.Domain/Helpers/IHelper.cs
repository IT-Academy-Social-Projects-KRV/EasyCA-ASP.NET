using CrudMicroservice.Data.Entities;

namespace CrudMicroservice.Domain.Helpers
{
    public interface IHelper
    {
        public User GetUser(string userId);
    }
}
