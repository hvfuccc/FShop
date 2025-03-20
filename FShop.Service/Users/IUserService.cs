using FShop.Dto.Users;

namespace FShop.Service.Users
{
    public interface IUserService
    {
        Task<string> Login(LoginRequest loginRequest);

        Task<bool> Register(RegisterRequest registerRequest);
    }
}