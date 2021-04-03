using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TrallyRally.Models;
using TrallyRally.Entities;

namespace TrallyRally.Services
{
    public interface IUserService
    {
        IUser AttemptLogin(string username, string password);
        IUser GetById(int id);
        IUser GetUserFromClaims(ClaimsPrincipal claimsPrincipal);
    }

    public class PlayerAuthService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<IUser> _users = new List<IUser>
        {
            new Player { ID = 1, Phone = "1234", Password = "test" }
        };

        public IUser AttemptLogin(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            return user;
        }

        public IUser GetById(int id)
        {
            return _users.FirstOrDefault(x => x.ID == id);
        }

        public IUser GetUserFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return GetById(Int32.Parse(userIdClaim.Value));
        }
    }
}