using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Data.Interfaces;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AccountService.Domain.Errors;
using AccountService.Domain.Interfaces;
using AccountService.Domain.Properties;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MongoDB.Driver;

namespace AccountService.Domain.Services
{
    public class ServiceAccount : IServiceAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IGenericRepository<PersonalData> _personalData;

        public ServiceAccount(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IMapper mapper, IEmailService emailService, IGenericRepository<PersonalData> personalData)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _emailService = emailService;
            _personalData = personalData;

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

                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("RegistrationFailed"));
            }
            else
            {
                List<IdentityError> identityErrors = result.Errors.ToList();
                var errors = string.Join(" ", identityErrors.Select(x => x.Description));

                throw new RestException(HttpStatusCode.BadRequest, errors);
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
        public async Task<ResponseApiModel<HttpStatusCode>> CreatePersonalData(PersonalDataRequestModel data, string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var personalData = _mapper.Map<PersonalData>(data);

            await _personalData.CreateAsync(personalData);

            user.PersonalDataId = personalData.Id;

            await _userManager.UpdateAsync(user);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Creating personal data is success!");
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdatePersonalData(PersonalDataRequestModel data, string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var personalData = _mapper.Map<PersonalData>(data);
            personalData.Id = user.PersonalDataId;
            await _personalData.ReplaceAsync(x => x.Id == user.PersonalDataId, personalData);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Update personal data is success!");
        }

        public async Task<PersonalDataResponseModel> GetPersonalData(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var personalDataId = user.PersonalDataId;

            if (personalDataId == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserPersonalDataNotFound"));
            }

            var personalData = await _personalData.GetByFilterAsync(x => x.Id == personalDataId);
            var response = _mapper.Map<PersonalDataResponseModel>(personalData);

            return response;
        }

        public async Task<UserResponseModel> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var persData = await _personalData.GetByFilterAsync(x => x.Id == user.PersonalDataId);
            var mapedPersData = _mapper.Map<PersonalDataResponseModel>(persData);
            var mappedUser = _mapper.Map<UserResponseModel>(user);
            mappedUser.PersonalData = mapedPersData;

            return mappedUser;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
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

        public async Task<ResponseApiModel<HttpStatusCode>> ForgotPassword(ForgotPasswordApiModel data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserNotFound"));
            }

            if (data.NewPassword != data.ConfirmPassword)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("PasswordsNotMatching"));
            }

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var tokenBytes = Encoding.UTF8.GetBytes(resetPasswordToken);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

            var passwordBytes = Encoding.UTF8.GetBytes(data.NewPassword);
            var encodedPassword = WebEncoders.Base64UrlEncode(passwordBytes);

            var param = new Dictionary<string, string>
            {
                { "password", encodedPassword },
                { "token", encodedToken },
                { "email", data.Email }
            };

            var callback = QueryHelpers.AddQueryString(data.PasswordURI, param);
            var emailResult = await _emailService.SendEmailAsync(data.Email, "EasyCA-Restore Your Password", callback);

            if (emailResult.Success)
            {
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("RestoreLinkSent"));
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("RestoreLinkNotSent"));
            }
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RestorePassword(string newPassword, string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var decodedPasswordBytes = WebEncoders.Base64UrlDecode(newPassword);
            var normalPassword = Encoding.UTF8.GetString(decodedPasswordBytes);

            var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
            var normalToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, normalPassword);

            if (result.Succeeded)
            {
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("PasswordResetSuccess"));
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, string.Join("\n", result.Errors));
            }
        }

        public async Task<ResponseApiModel<HttpStatusCode>> ChangePassword(string password, string oldPassword, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, password);

            if (result.Succeeded)
            {
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Password was changed successfully");
            }
            else
            {
                List<IdentityError> identityErrors = result.Errors.ToList();
                var errors = string.Join(" ", identityErrors.Select(x => x.Description));

                throw new RestException(HttpStatusCode.BadRequest, errors);
            }
        }

        public async Task<UserResponseModel> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var persData = await _personalData.GetByFilterAsync(x => x.Id == user.PersonalDataId);
            var mapedPersData = _mapper.Map<PersonalDataResponseModel>(persData);
            var mappedUser = _mapper.Map<UserResponseModel>(user);
            mappedUser.PersonalData = mapedPersData;

            return mappedUser;
        }
    }
}
