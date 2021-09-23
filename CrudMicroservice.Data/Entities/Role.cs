using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace CrudMicroservice.Data.Entities
{
    [CollectionName("Roles")]
    public class Role: MongoIdentityRole<string>
    {
    }
}
