using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AccountService.Data.Entities
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonalData UserData { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }
}

