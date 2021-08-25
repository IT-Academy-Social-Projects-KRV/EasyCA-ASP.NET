namespace AccountService.Domain.ApiModel.RequestApiModels
{
    public class LoginApiModel
    {
        /// <summary>
        /// User email
        /// </summary>     
        /// <example>test@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        /// <example>QWerty-1</example>
        public string Password { get; set; }
    }
}
