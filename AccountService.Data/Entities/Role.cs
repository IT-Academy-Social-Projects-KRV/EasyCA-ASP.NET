using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AccountService.Data.Entities
{
    [CollectionName("Roles")]
    public class Role : MongoIdentityRole<string>
    {
    }
}
