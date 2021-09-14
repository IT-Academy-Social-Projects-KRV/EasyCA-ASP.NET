namespace AccountService.Domain.ApiModel.RequestApiModels
{
    public class ForgotPasswordApiModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordURI { get; set; }
    }
}
