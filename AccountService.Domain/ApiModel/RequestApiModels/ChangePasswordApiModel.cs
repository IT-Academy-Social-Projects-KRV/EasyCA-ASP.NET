namespace AccountService.Domain.ApiModel.RequestApiModels
{
    public class ChangePasswordApiModel
    {
        public string Password { get; set; }
        public string OldPassword { get; set; }
    }
}
