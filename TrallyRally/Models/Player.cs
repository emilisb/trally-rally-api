using System;
using System.Text.Json.Serialization;
using TrallyRally.Entities;

namespace TrallyRally.Models
{
    public class Player : IUser
    {
        public int ID { get; set; }
        public string Username {
            get { return Phone; }
            set { throw new NotSupportedException("Operation not supported"); }
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public int StartPosition { get; set; }
        public DateTime StartTime { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
