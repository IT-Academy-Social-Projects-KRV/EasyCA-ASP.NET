using System;

namespace AccountService.Data.Entities
{
    public class PersonalData
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IPN { get; set; }
        public string ServiceID { get; set; }
        public DateTime BirthDay { get; set; }
        public string JobPosition { get; set; }
    }
}
