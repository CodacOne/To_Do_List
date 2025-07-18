using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;


namespace To_Do_List.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.GetUserByUsernameAsync(model.Username);

            if (user == null || user.PasswordHash != model.Password)
                return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }


        [HttpPost("register")]
        
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var existingUser = await _userService.GetUserByUsernameAsync(model.UserName);
            if (existingUser != null)
                return BadRequest("Username already exists");

            var user = new clsUser
            {
                Username = model.UserName,
                PasswordHash = model.Password,
                Role = model.Role
            };

            await _userService.AddUserAsync(user);

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }

    }

    public class RegisterModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

       
        public string Role { get; set; } = "Guest";  // أو "Owner"
    }


    public class LoginModel
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
