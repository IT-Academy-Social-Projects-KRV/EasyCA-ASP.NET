using AccountService.Data.Entities;

namespace AccountService.Domain.ApiModel.RequestApiModels
{
    public class UserRequestModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonalData UserData { get; set; }
    }
}
