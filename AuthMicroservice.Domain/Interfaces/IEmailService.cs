using System.Net;
using System.Threading.Tasks;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;

namespace AuthMicroservice.Domain.Interfaces
{
    public interface IEmailService
    {
        Task<ResponseApiModel<HttpStatusCode>> SendEmailAsync(string userEmail, string emailSubject, string message);
    }
}
