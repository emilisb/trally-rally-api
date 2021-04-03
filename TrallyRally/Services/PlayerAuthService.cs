using System.Collections.Generic;
using System.Linq;
using TrallyRally.Models;
using TrallyRally.Entities;

namespace TrallyRally.Services
{
    public interface IUserService
    {
        IUser AttemptLogin(string username, string password);
        IEnumerable<IUser> GetAll();
        IUser GetById(int id);
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

        public IEnumerable<IUser> GetAll()
        {
            return _users;
        }

        public IUser GetById(int id)
        {
            return _users.FirstOrDefault(x => x.ID == id);
        }
    }
}