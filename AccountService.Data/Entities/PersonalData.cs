using System;

namespace AccountService.Data.Entities
{
    public class PersonalData
    {
        public string UserId { get; set; }
        public string Address { get; set; }
        public string IPN { get; set; }
        public string ServiceId { get; set; }
        public DateTime BirthDay { get; set; }
        public string JobPosition { get; set; }
        public DriverLicense UserDriverLicense { get; set; }
    }
}
