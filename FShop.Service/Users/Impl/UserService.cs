using FShop.Data.Entities;
using FShop.Dto.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FShop.Service.Users.Impl
{
    public class UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IConfiguration config) : IUserService
    {
        public async Task<string> Login(LoginRequest loginRequest)
        {
            var user = await userManager.FindByNameAsync(loginRequest.Username);
            if (user == null) return null;
            var result = await signInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.RememberMe, true);
            if (!result.Succeeded) return null;
            var roles = await userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Firstname),
                new Claim(ClaimTypes.Role, string.Join(";", roles))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(config["Tokens:Issuer"],
                config["Tokens:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(config["Tokens:TokenValidityMins"])),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterRequest registerRequest)
        {
            var user = new User()
            {
                Firstname = registerRequest.FirstName,
                Lastname = registerRequest.LastName,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                UserName = registerRequest.UserName,
                Dob = registerRequest.Dob
            };
            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (result.Succeeded) return true;
            return false;
        }
    }
}