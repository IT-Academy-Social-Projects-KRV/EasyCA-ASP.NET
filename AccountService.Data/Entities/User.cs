    using System;
    using AspNetCore.Identity.MongoDbCore.Models;

    namespace AccountService.Data.Entities
    {
        public class User : MongoIdentityUser
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public PersonalData UserData { get; set; }
        }
    }
    
