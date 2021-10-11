namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class UserResponseApiModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonalDataResponseApiModel PersonalData { get; set; }
    }
}
