using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace FileMicroservice.Data.Entities
{
    [CollectionName("Roles")]
    public class Role: MongoIdentityRole<string>
    {
    }
}
