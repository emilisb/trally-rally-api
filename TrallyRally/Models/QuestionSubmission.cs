using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrallyRally.Models
{
    public class QuestionSubmission
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public int PlayerID { get; set; }
        public int GameID { get; set; }
        public string Answer { get; set; }
        public bool? Correct { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }

        [JsonIgnore]
        public Question Question { get; set; }
    }
}
