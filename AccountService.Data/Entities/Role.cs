using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AccountService.Data.Entities
{
    [CollectionName("Roles")]
    public class Role : MongoIdentityRole<string>
    {

    }
}
