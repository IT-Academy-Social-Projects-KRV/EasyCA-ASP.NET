using System.Net;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AccountService.Domain.Errors;
using AccountService.Domain.Interfaces;
using AccountService.Domain.Properties;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Domain.Services
{
    public class ServiceAccount : IServiceAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        public ServiceAccount(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterApiModel userRequest)
        {

            User user = new User()
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                UserName = userRequest.Email,
            };

            var result = await _userManager.CreateAsync(user, userRequest.Password);

            if (result.Succeeded)
            {                
                await _userManager.AddToRoleAsync(user, "participant");
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.RegistrationSucceeded);
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.RegistrationFailed);
            }
        }

        public async Task<AuthenticateResponseApiModel> LoginUser(LoginApiModel userRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(userRequest.Email, userRequest.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userRequest.Email);

                if (user == null)
                {
                    return null;
                }

                var token = _jwtService.CreateJwtToken(user);
                var refreshtoken = _jwtService.CreateRefreshToken();
                user.RefreshToken = refreshtoken;
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, false);

                return new AuthenticateResponseApiModel(user.Email, token, refreshtoken.Token);
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.LoginWrongCredentials);
            }
        }

        public async Task<PersonalDataApiModel> GetPersonalData(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.UserNotFound);
            }

            var personalData = user.UserData;

            if (personalData == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.UserPersonalDataNotFound);
            }

            var response = new PersonalDataApiModel()
            {
                Address = personalData.UserAddress,
                IPN = personalData.IPN,
                BirthDay = personalData.BirthDay,
                ServiceNumber = personalData.ServiceNumber,
                UserDriverLicense = personalData.UserDriverLicense,
                JobPosition = personalData.JobPosition,
                UserCars = personalData.UserCars
            };

            return response;
        }
    }
}
