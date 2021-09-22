namespace AuthMicroservice.Domain.ApiModel.EmailConfirmApiModels
{
    public class SendEmailResponceApiModel
    {
        public bool Successful => ErrorMsg == null;
        public string ErrorMsg { get; set; }
    }
}
