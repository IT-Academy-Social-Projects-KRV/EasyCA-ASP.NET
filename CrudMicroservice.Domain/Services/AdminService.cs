using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.Domain.Properties;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrudMicroservice.Domain.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<EuroProtocol> _euroProtocols;
        private readonly IGenericRepository<CarAccident> _CAProtocols;

        public AdminService(UserManager<User> userManager, IMapper mapper, IGenericRepository<EuroProtocol> euroProtocols, IGenericRepository<CarAccident> CAProtocols)
        {
            _userManager = userManager;
            _mapper = mapper;
            _CAProtocols = CAProtocols;
            _euroProtocols = euroProtocols;
        }

        public async Task<IEnumerable<EuroProtocolResponseApiModel>> GetAllEuroProtocols()
        {
            var list = await _euroProtocols.GetAllAsync();

            if (!list.AsQueryable().Any())
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("EuroProtocolsNotFound"));
            }

            return _mapper.Map<IEnumerable<EuroProtocolResponseApiModel>>(list);
        }

        public async Task<IEnumerable<CarAccidentResponseApiModel>> GetAllCAProtocols()
        {
            var list = await _CAProtocols.GetAllAsync();

            if (!list.AsQueryable().Any())
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("CAprotocolNotFound"));
            }

            return _mapper.Map<IEnumerable<CarAccidentResponseApiModel>>(list);
        }

        public async Task<IEnumerable<UserResponseApiModel>> GetAllInspectors()
        {
            var list = await _userManager.GetUsersInRoleAsync("inspector");

            if (!list.AsQueryable().Any())
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("InspectorsNotFound"));
            }

            return _mapper.Map<IEnumerable<UserResponseApiModel>>(list);
        }

        public async Task<ResponseApiModel<HttpStatusCode>> AddInspector(InspectorRequestApiModel inspectorRequest)
        {
            var inspector = _mapper.Map<User>(inspectorRequest);
            var result = await _userManager.CreateAsync(inspector, inspectorRequest.Password);

            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(inspector);
                await _userManager.ConfirmEmailAsync(inspector, confirmEmailToken);

                await _userManager.AddToRoleAsync(inspector, "inspector");
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("RegistrationSucceeded"));
            }

            throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("RegistrationFailed"));
        }

        public async Task<ResponseApiModel<HttpStatusCode>> DeleteInspector(DeleteInspectorRequestApiModel data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserNotFound"));
            }
           
            await _userManager.RemoveFromRoleAsync(user, "inspector");
            await _userManager.AddToRoleAsync(user, "participant");

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("InspectorDeleteSucceeded"));
        }
    }
}
