using System;
using System.Collections.Generic;

namespace TrallyRally.Models
{
    public class Game
    {
        public Game()
        {
            this.Players = new HashSet<Player>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
