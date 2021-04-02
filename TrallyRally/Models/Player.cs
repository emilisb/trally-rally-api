using System;
namespace TrallyRally.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public int StartPosition { get; set; }
        public DateTime StartTime { get; set; }
    }
}
