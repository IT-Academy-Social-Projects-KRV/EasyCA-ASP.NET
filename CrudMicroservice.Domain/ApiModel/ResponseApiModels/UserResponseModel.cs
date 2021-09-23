namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class UserResponseModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonalDataResponseModel PersonalData { get; set; }
    }
}
