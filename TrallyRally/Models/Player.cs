using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TrallyRally.Entities;

namespace TrallyRally.Models
{
    public class Player : IUser
    {
        public Player()
        {
            this.Games = new HashSet<Game>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public int StartPosition { get; set; }
        public DateTime StartTime { get; set; }

        public ICollection<Game> Games { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [NotMapped]
        public string Username
        {
            get { return Phone; }
            set { throw new NotSupportedException("Operation not supported"); }
        }
    }
}
