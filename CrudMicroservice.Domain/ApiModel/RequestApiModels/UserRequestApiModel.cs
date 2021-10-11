namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class UserRequestApiModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonalDataRequestApiModel PersonalData { get; set; }
    }
}