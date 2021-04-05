using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TrallyRally.Entities;
using TrallyRally.Data;

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
        private readonly DatabaseContext _context;

        public PlayerAuthService(DatabaseContext context)
        {
            _context = context;
        }

        public IUser AttemptLogin(string username, string password)
        {
            var user = _context.Players.FirstOrDefault(x => x.Phone == username);
            if (user == null)
            {
                return null;
            }

            var passwordVerificationResult = new PasswordHasher<IUser>().VerifyHashedPassword(user, user.Password, password);

            switch (passwordVerificationResult)
            {
                case PasswordVerificationResult.Success:
                case PasswordVerificationResult.SuccessRehashNeeded:
                    return user;
                case PasswordVerificationResult.Failed:
                default:
                    return null;
            }
        }

        public IUser GetById(int id)
        {
            return _context.Players.Find(id);
        }

        public IUser GetUserFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return GetById(Int32.Parse(userIdClaim.Value));
        }
    }
}