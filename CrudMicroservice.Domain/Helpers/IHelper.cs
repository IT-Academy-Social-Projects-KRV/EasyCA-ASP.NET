using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudMicroservice.Data.Entities;

namespace CrudMicroservice.Domain.Helpers
{
    public interface IHelper
    {
        public User GetUser(string userId);
    }
}
