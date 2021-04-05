using System;
using TrallyRally.Models;

namespace TrallyRally.Dtos
{
    public class PlayerDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public int StartPosition { get; set; }
        public DateTime StartTime { get; set; }

        public PlayerDto(Player player)
        {
            ID = player.ID;
            Name = player.Name;
            Phone = player.Phone;
            Avatar = player.Avatar;
            StartPosition = player.StartPosition;
            StartTime = player.StartTime;
        }
    }
}
