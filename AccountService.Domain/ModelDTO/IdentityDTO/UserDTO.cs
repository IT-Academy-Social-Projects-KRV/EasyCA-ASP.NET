namespace AccountService.Domain.ApiModel.IdentityDTO
{
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the primary key for this user.
        /// </summary>
        public string Id { get; set; }
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
        /// Gets or sets the normalized user name for this user.
        /// </summary>
        public string NormalizedUserName { get; set; }
        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets a salted and hashed representation of the password for this user.
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Gets or sets a telephone number for the user.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
