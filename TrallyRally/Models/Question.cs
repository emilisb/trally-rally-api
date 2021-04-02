using System;
using System.Collections.Generic;

namespace TrallyRally.Models
{
    public enum QuestionType
    {
        INPUT, PHOTO, QR
    }

    public class Question
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public int Points { get; set; }
        public int MaxDistance { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public ICollection<QuestionSubmission> QuestionSubmissions { get; set; }
    }
}