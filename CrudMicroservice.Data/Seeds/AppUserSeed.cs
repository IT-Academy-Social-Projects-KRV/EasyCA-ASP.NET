using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudMicroservice.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace CrudMicroservice.Data.Seeds
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
                        PersonalDataId = null,
                        UserName = "Test@gmail.com",
                        Email="Test@gmail.com",
                        RefreshToken = null,
                        EmailConfirmed = true
                    },
                    new User
                    {
                        FirstName = "Dmytro",
                        LastName = "Pinkevych",
                        PersonalDataId = null,
                        UserName = "pinkevych@gmail.com",
                        Email="pinkevych@gmail.com",
                        RefreshToken = null,
                        EmailConfirmed = true
                    },
                    new User
                    {
                        FirstName = "Ivan",
                        LastName = "Kosmin",
                        PersonalDataId = null,
                        UserName = "kosminfeed@gmail.com",
                        Email="kosminfeed@gmail.com",
                        RefreshToken = null,
                        EmailConfirmed = true
                    },
                    new User
                    {
                        FirstName = "Sasha",
                        LastName = "Korniichuk",
                        PersonalDataId = null,
                        UserName = "okorniichuk03@gmail.com",
                        Email="okorniichuk03@gmail.com",
                        RefreshToken = null,
                        EmailConfirmed = true
                    },
                    new User
                    {
                        FirstName = "Liza",
                        LastName = "Shemetovska",
                        PersonalDataId = null,
                        UserName = "Shemetovska@gmail.com",
                        Email="Shemetovska@gmail.com",
                        RefreshToken = null,
                        EmailConfirmed = true
                    }
                };
                foreach (var user in users)
                {
                    await manager.CreateAsync(user, "Qwerty211@");
                    await manager.AddToRoleAsync(user, "admin");
                }
            }
        }
    }
}
