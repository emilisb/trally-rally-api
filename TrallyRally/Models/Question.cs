using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TrallyRally.Dtos;

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
        public string? Image { get; set; }
        public string Text { get; set; }

        [JsonIgnore]
        public string? Answer { get; set; }

        public QuestionType Type { get; set; }
        public int Points { get; set; }
        public int MaxDistance { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<Game> Games { get; set; }
        public IList<QuestionSubmission> QuestionSubmissions { get; set; }

        public QuestionDto ConvertToDto()
        {
            return new QuestionDto(this);
        }
    }
}