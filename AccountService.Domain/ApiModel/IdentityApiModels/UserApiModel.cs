using AccountService.Data.Entities;

namespace AccountService.Domain.ApiModel.IdentityApiModel
{
    public class UserApiModel
    {
        /// <summary>
        /// First name.
        /// </summary>     
        /// <example>TestName</example>
        public string FirstName { get; set; }
        /// <summary>
        /// First name.
        /// </summary>     
        /// <example>TestName</example>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the Personal data for this user.
        /// </summary>
        public PersonalData UserData { get; set; }
    }
}
