﻿using System.Net;
using System.Threading.Tasks;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;

namespace AccountService.Domain.Interfaces
{
    public interface IServiceAccount
    {
        Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterApiModel user);
        Task<AuthenticateResponseApiModel> LoginUser(LoginApiModel userRequest);
        Task<ResponseApiModel<HttpStatusCode>> UpdateUserData(PersonalDataRequestModel data, string userId);
        Task<PersonalDataResponseModel> GetPersonalData(string userId);
        Task<UserResponseModel> GetUserById(string userId);
    }
}
