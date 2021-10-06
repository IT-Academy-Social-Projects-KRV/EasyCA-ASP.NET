using System.Net;
using System.Threading.Tasks;
using AuthMicroservice.Domain.ApiModel.RequestApiModels;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;

namespace AuthMicroservice.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterApiModel user);
        Task<AuthenticateResponseApiModel> LoginUser(LoginApiModel userRequest);
        Task<ResponseApiModel<HttpStatusCode>> ConfirmEmailAsync(string userId, string token);
        Task<ResponseApiModel<HttpStatusCode>> ForgotPassword(ForgotPasswordApiModel data);
        Task<ResponseApiModel<HttpStatusCode>> RestorePassword(string newPassword, string token, string email);
        Task<ResponseApiModel<HttpStatusCode>> RegisterInspector(RegisterApiModel inspectorRequest);
    }
}
