using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.Domain.Properties;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace CrudMicroservice.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<PersonalData> _personalData;

        public AccountService(UserManager<User> userManager, IMapper mapper, IGenericRepository<PersonalData> personalData)
        {
            _userManager = userManager;
            _mapper = mapper;
            _personalData = personalData;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> CreatePersonalData(PersonalDataRequestModel data, string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            if (!string.IsNullOrEmpty(user.PersonalDataId))
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("UserDataExists"));
            }

            var personalData = _mapper.Map<PersonalData>(data);

            await _personalData.CreateAsync(personalData);

            user.PersonalDataId = personalData.Id;

            await _userManager.UpdateAsync(user);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Creating personal data is success!");
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdatePersonalData(UserRequestModel data, string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            if (string.IsNullOrEmpty(user.PersonalDataId))
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("UserDataNotExist"));
            }

            var personalData = _mapper.Map<PersonalDataRequestModel, PersonalData>(data.PersonalData);
            personalData.Id = user.PersonalDataId;
            var result = await _personalData.ReplaceAsync(x => x.Id == user.PersonalDataId, personalData);

            if (!result.IsAcknowledged)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("PersonalDataNotUpdate"));
            }     
            
            user.Email = data.Email;
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.UserName = data.Email;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("UserDataNotUpdate"));
            }

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Update personal data is success!");
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
            
            return _mapper.Map<PersonalDataResponseModel>(personalData);
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
