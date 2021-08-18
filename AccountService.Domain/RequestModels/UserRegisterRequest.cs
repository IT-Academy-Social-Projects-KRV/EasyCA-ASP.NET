using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.RequestModels
{
    public class UserRegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IPN { get; set; }
        public DateTime BirthDay { get; set; }
        public string Job { get; set; }
        public string Citizen { get; set; }
        public string Password { get; set; }
        public string ServiceId { get; set; }

    }
}
