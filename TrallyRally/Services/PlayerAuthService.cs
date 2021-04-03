using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;
using TrallyRally.Helpers;
using TrallyRally.Models;
using TrallyRally.Entities;

namespace TrallyRally.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<IUser> GetAll();
        IUser GetById(int id);
    }

    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateResponse
    {
        public int ID { get; set; }
        public string Token { get; set; }
        public IUser User { get; set; }


        public AuthenticateResponse(IUser user, string token)
        {
            ID = user.ID;
            Token = token;
            User = user;
        }
    }

    public class PlayerAuthService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<IUser> _users = new List<IUser>
        {
            new Player { ID = 1, Phone = "1234", Password = "test" }
        };

        private readonly AppSettings _appSettings;

        public PlayerAuthService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<IUser> GetAll()
        {
            return _users;
        }

        public IUser GetById(int id)
        {
            return _users.FirstOrDefault(x => x.ID == id);
        }

        private string generateJwtToken(IUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}