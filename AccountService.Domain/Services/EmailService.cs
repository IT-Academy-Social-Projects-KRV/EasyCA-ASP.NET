using System.Net;
using System.Threading.Tasks;
using AccountService.Domain.ApiModel.EmailConfirmApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AccountService.Domain.Errors;
using AccountService.Domain.Interfaces;
using AccountService.Domain.Properties;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AccountService.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> SendEmailAsync(string userEmail,string emailSubject,string message)
        {
            var apiKey = _configuration.GetValue<string>("SendGridKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("OKorniichuk03@gmail.com");
            var subject = emailSubject;
            var to = new EmailAddress(userEmail);
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var result= await client.SendEmailAsync(msg);
            if(result.IsSuccessStatusCode)
            {
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("EmailSend"));
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("EmailNotSend"));
            }          
        }
    }
}
