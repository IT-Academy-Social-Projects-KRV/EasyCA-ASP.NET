using Microsoft.AspNet.Identity.EntityFramework;
using MongoDB.Bson;

namespace AccountService.Data.Entities
{
    public class User: IdentityUser
    {
        public ObjectId Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string Email { get; set; }
        public override string PasswordHash { get; set; }
        public PersonalData UserData { get; set; }
        public DriverLicense UserDriverLicense { get; set; }
    }
}
