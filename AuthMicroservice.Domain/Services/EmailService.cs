using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;
using AuthMicroservice.Domain.Errors;
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Domain.Properties;
using Microsoft.Extensions.Configuration;

namespace AuthMicroservice.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> SendEmailAsync(string userEmail, string emailSubject, string message)
        {
            using SmtpClient smtp = new();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("easyca.mailservice@gmail.com", "Qwerty211@");
            smtp.EnableSsl = true;
            smtp.Timeout = 3000;

            var from = new MailAddress("noreply@ichtlay.site");
            var to = new MailAddress(userEmail);

            MailMessage mailMessage = new(from, to);
            mailMessage.Subject = emailSubject;
            mailMessage.Body = message;
            try
            {
                await smtp.SendMailAsync(mailMessage);
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("EmailSend"));
            }
            catch (Exception)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("EmailNotSend"));
            }
        }
    }
}
