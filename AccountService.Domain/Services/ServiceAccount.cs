using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AccountService.Domain.Errors;
using AccountService.Domain.Interfaces;
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

        public async Task RegisterUser(RegisterApiModel userRequest)
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
                if (user.UserData.ServiceNumber == null)
                {
                    await _userManager.AddToRoleAsync(user, "participant");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "inspector");
                }
            }
            else
            {
                throw new ArgumentException("Result error");
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
                throw new RestException(HttpStatusCode.BadRequest,"Email or Password are wrong");
            }
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdateUserData(PersonalDataApiModel data, string userId)
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
    }
}
