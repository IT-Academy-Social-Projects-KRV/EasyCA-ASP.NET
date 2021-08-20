using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Domain.Interfaces;
using AccountService.Domain.RequestModels;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Domain.Services
{
    public class ServiceAccount : IServiceAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ServiceAccount(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task RegisterUser(UserRegisterRequest userRequest)
        {
            User user = new User()
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                UserData = new PersonalData()
                {
                    Address = userRequest.Address,
                    IPN = userRequest.IPN,
                    ServiceId = userRequest.ServiceId,
                    BirthDay = userRequest.BirthDay,
                    JobPosition = userRequest.Job,
                    UserDriverLicense = new DriverLicense()
                },
                Email = userRequest.Email,
                UserName = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,
            };


            var result = await _userManager.CreateAsync(user, userRequest.Password);
            if (result.Succeeded)
            {
                if (user.UserData.ServiceId == null)
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

        public async Task LoginUser(UserLoginRequest userRequest)
        {
            var result=await _signInManager.PasswordSignInAsync(userRequest.Email, userRequest.Password,false,false);
            if(result.Succeeded)
            {
               var user = await _userManager.FindByEmailAsync(userRequest.Email);
               await _signInManager.SignInAsync(user, false);
            }
            else
            {
                throw new ArgumentException("Result error");
            }
        }
    }
}
