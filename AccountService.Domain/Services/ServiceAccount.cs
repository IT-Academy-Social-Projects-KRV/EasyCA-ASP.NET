using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AccountService.Domain.Errors;
using AccountService.Domain.Interfaces;
using AccountService.Domain.Properties;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace AccountService.Domain.Services
{
    public class ServiceAccount : IServiceAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ServiceAccount(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IMapper mapper, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterApiModel userRequest)
        {
            User user = _mapper.Map<User>(userRequest);

            var result = await _userManager.CreateAsync(user, userRequest.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "participant");

                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                var param = new Dictionary<string, string>
                {
                    {"token",validEmailToken },
                    {"email",user.Email }
                };

                var callback = QueryHelpers.AddQueryString(userRequest.ClientURI, param);

                var emailResult = await _emailService.SendEmailAsync(user.Email, "EasyCA-Confirm Your Email", callback);

                if (emailResult.Success)
                {
                    return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("RegistrationSucceeded"));
                }

                throw new RestException(HttpStatusCode.BadRequest, string.Join("\n", result.Errors));
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, string.Join("\n", result.Errors));
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

                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("EmailnotConfirmed"));
                }

                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtService.CreateJwtToken(user);
                var refreshtoken = _jwtService.CreateRefreshToken();

                user.RefreshToken = refreshtoken;

                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, false);

                return new AuthenticateResponseApiModel(user.Email, token, refreshtoken.Token, roles.FirstOrDefault());
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("LoginWrongCredentials"));
            }
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdateUserData(UserRequestModel data, string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            user.UserData = data.UserData;
            user.Email = data.Email;
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;

            await _userManager.UpdateAsync(user);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Update personal data is success!");
        }

        public async Task<PersonalDataResponseModel> GetPersonalData(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var personalData = user.UserData;

            if (personalData == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserPersonalDataNotFound"));
            }

            var response = _mapper.Map<PersonalDataResponseModel>(personalData);

            return response;
        }

        public async Task<UserResponseModel> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var mappedUser = _mapper.Map<UserResponseModel>(user);

            return mappedUser;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> CreatePersonalData(PersonalDataRequestModel data, string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            var persData = _mapper.Map<PersonalData>(data);
            user.UserData = persData;

            await _userManager.UpdateAsync(user);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Creating personal data is success!");
        }

        public async Task<ResponseApiModel<HttpStatusCode>> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserNotFound"));

            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Email confirm successfully!");
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("LoginWrongCredentials"));
            }

        }
    }
}
