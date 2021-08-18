using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Data.Seeds
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
                    },
                    new Role
                    {
                        Name = "guest",
                    },
                };
                foreach (var role in roles)
                {
                    await manager.CreateAsync(role);
                }
            }
        }
    }
}
