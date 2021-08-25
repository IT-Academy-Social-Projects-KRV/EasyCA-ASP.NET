using System.Threading.Tasks;
using AccountService.Domain.RequestModels;

namespace AccountService.Domain.Interfaces
{
    public interface IServiceAccount
    {
        public Task RegisterUser(UserRegisterRequest user);
        public Task LoginUser(UserLoginRequest user);
    }
}
