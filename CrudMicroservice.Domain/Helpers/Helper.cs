using System.Linq;
using CrudMicroservice.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace CrudMicroservice.Domain.Helpers
{
    public class Helper : IHelper
    {
        private readonly UserManager<User> _userManager;
        public Helper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public User GetUser(string userId)
        {
            return _userManager.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}
