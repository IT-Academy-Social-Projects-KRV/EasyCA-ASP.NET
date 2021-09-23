using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthMicroservice.Data.Entities
{
    [CollectionName("Roles")]
    public class Role: MongoIdentityRole<string>
    {
    }
}
