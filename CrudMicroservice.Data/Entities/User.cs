using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace CrudMicroservice.Data.Entities
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalDataId { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
