namespace AccountService.Domain.ApiModel.RequestApiModels
{
    public class UserRequestModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonalDataRequestModel PersonalData { get; set; }
    }
}
