using System;
using System.ComponentModel.DataAnnotations;

namespace TrallyRally.Models
{
    public class QuestionSubmission
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public int PlayerID { get; set; }
        public string Answer { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime SubmissionTime { get; set; }


        public Player Player { get; set; }
    }
}
