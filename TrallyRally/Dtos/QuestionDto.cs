using System;
using TrallyRally.Models;
using TrallyRally.DataStructures;

namespace TrallyRally.Dtos
{
    public class QuestionDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public int Points { get; set; }
        public int MaxDistance { get; set; }
        public Coordinates Coordinates { get; set; }
        public string LastAnswer { get; set; }
        public bool Submitted { get; set; }
        public bool Locked { get; set; }

        public QuestionDto(Question question)
        {
            ID = question.ID;
            Title = question.Title;
            Image = question.Image;
            Text = question.Text;
            Type = Enum.GetName(typeof(QuestionType), question.Type);
            Points = question.Points;
            MaxDistance = question.MaxDistance;
            Coordinates = new Coordinates { Lat = question.Latitude, Long = question.Longitude };

            LastAnswer = question.QuestionSubmissions.Count > 0 ? question.QuestionSubmissions[0].Answer : null;
            Submitted = LastAnswer != null;
        }
    }
}
