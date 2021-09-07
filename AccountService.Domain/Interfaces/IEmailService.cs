using System.Net;
using System.Threading.Tasks;
using AccountService.Domain.ApiModel.ResponseApiModels;

namespace AccountService.Domain.Interfaces
{
    public interface IEmailService
    {
        Task<ResponseApiModel<HttpStatusCode>> SendEmailAsync(string userEmail, string emailSubject, string message);
    }
}
