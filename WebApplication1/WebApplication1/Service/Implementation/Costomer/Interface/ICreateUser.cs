using IncomeBridgeAPI.Common;
using IncomeBridgeAPI.Service.Implementation.Costomer.Model;

namespace IncomeBridgeAPI.Service.Implementation.Costomer.Interface
{
    public interface ICreateUser
    {
        Task<UserResponse> RegisterAsync(UserDTO registrationRequest);
    }
}
