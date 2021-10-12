using System.Net;
using System.Threading.Tasks;
using AuthMicroservice.Domain.ApiModel.RequestApiModels;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;

namespace AuthMicroservice.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterRequestApiModel user);
        Task<AuthenticateResponseApiModel> LoginUser(LoginRequestApiModel userRequest);
        Task<ResponseApiModel<HttpStatusCode>> ConfirmEmailAsync(string userId, string token);
        Task<ResponseApiModel<HttpStatusCode>> ForgotPassword(ForgotPasswordRequestApiModel data);
        Task<ResponseApiModel<HttpStatusCode>> RestorePassword(string newPassword, string token, string email);
        Task<ResponseApiModel<HttpStatusCode>> ResendConfirmation(ResendConfirmationRequestApiModel data);
    }
}
