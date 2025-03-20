using FShop.Dto.Users;
using FShop.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FShop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var resultToken = await userService.Login(loginRequest);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username hoặc Mật khẩu chưa chính xác");
            }
            return Ok(new { token = resultToken });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await userService.Register(registerRequest);
            if (!result)
                return BadRequest("Không thể đăng ký");
            return Ok(result);
        }
    }
}