using System;
using System.Text.Json.Serialization;
namespace TrallyRally.Entities
{
    public interface IUser
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
