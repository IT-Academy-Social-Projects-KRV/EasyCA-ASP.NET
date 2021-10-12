namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class ChangePasswordRequestApiModel
    {
        public string Password { get; set; }
        public string OldPassword { get; set; }
    }
}
