using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthMicroservice.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthMicroservice.Data.Seeds
{
    public class AppRoleSeed
    {
        public static async Task SeedRoleData(RoleManager<Role> manager)
        {
            if (!manager.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = "admin",
                    },
                    new Role
                    {
                        Name = "inspector",
                    },
                    new Role
                    {
                        Name = "participant",
                    }
                };
                foreach (var role in roles)
                {
                    await manager.CreateAsync(role);
                }
            }
        }
    }
}
