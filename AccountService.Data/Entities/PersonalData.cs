using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Data.Entities
{
    public class PersonalData
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IPN { get; set; }
        public string ServiceID { get; set; }
        public DateTime BirthDayy { get; set; }
        public string JobPOsition { get; set; }
    }
}
