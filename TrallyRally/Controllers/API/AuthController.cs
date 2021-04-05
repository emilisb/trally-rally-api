using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using TrallyRally.Services;
using TrallyRally.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrallyRally.Controllers.API
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public IUser User { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;

        public AuthController(IUserService userService, IJwtAuthManager jwtAuthManager)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.AttemptLogin(request.Username, request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var role = "player"; // TODO: use enum
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.Username, claims, DateTime.Now);
            return Ok(new LoginResponse
            {
                User = user,
                Role = role,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok(_userService.GetUserFromClaims(User));
        }
    }
}
