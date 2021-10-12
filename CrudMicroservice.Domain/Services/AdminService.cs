using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.Domain.Properties;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CrudMicroservice.Domain.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<EuroProtocol> _euroProtocols;

        public AdminService(UserManager<User> userManager, IMapper mapper, IGenericRepository<EuroProtocol> euroProtocols)
        {
            _userManager = userManager;
            _mapper = mapper;
            _euroProtocols = euroProtocols;
        }

        public async Task<IEnumerable<EuroProtocolResponseModel>> GetAllEuroProtocols()
        {
            var list = await _euroProtocols.GetAllAsync();

            if(list == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("EuroProtocolsNotFound"));
            }

            return _mapper.Map<IEnumerable<EuroProtocolResponseModel>>(list);
        }

        public async Task<IEnumerable<UserResponseModel>> GetAllInspectors()
        {
            var list = await _userManager.GetUsersInRoleAsync("inspector");

            if (list == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("InspectorsNotFound"));
            }

            return _mapper.Map<IEnumerable<UserResponseModel>>(list);
        }

        public async Task<ResponseApiModel<HttpStatusCode>> DeleteInspector(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

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
