using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Data.Seeds
{
    public class AppUserSeed
    {
        public static async Task SeedUserData(UserManager<User> manager)
        {
            if (!manager.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        FirstName = "Test",
                        LastName = "Test",
                        UserData = null,
                        UserName = "Test@gmail.com",
                        Email="Test@gmail.com",
                        RefreshToken = null
                    }
                };
                foreach (var user in users)
                {
                    await manager.CreateAsync(user,"Qwerty211@");
                    await manager.AddToRoleAsync(user, "admin");
                }
            }
        }
    }
}
