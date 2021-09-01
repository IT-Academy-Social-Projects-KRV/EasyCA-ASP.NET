using AspNetCore.Identity.MongoDbCore.Models;

namespace AccountService.Data.Entities
{
    [CollectionName("Roles")]
    public class Role: MongoIdentityRole<string>
    {
    }
}
