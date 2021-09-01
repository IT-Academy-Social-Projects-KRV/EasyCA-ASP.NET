using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AccountService.Domain.Errors;
using AccountService.Domain.Interfaces;
using AccountService.Domain.Properties;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Domain.Services
{
    public class ServiceAccount : IServiceAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        
        public ServiceAccount(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterApiModel userRequest)
        {
            User user = _mapper.Map<User>(userRequest);

            var result = await _userManager.CreateAsync(user, userRequest.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "participant");

                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("RegistrationSucceeded"));
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
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtService.CreateJwtToken(user);
                var refreshtoken = _jwtService.CreateRefreshToken();
                user.RefreshToken = refreshtoken;
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, false);

                return new AuthenticateResponseApiModel(user.Email, token, refreshtoken.Token,roles.FirstOrDefault());
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("LoginWrongCredentials"));
            }
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdateUserData(PersonalDataRequestModel data, string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            user.UserData = new()
            {
                UserAddress = data.UserAddress,
                BirthDay = data.BirthDay,
                IPN = data.IPN,
                ServiceNumber = data.ServiceNumber,
                JobPosition = data.JobPosition,
                UserDriverLicense = data.UserDriverLicense,
                UserCars= data.UserCars
            };
            await _userManager.UpdateAsync(user);
            
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "��� ����������� ������ �����!");
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
    }
}