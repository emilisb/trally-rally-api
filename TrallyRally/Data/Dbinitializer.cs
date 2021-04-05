using TrallyRally.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace TrallyRally.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            SeedGames(context);
            SeedQuestions(context);
            SeedPlayers(context);
        }

        public static void SeedGames(DatabaseContext context)
        {
            if (context.Games.Any())
            {
                return;
            }

            var games = new Game[]
            {
                new Game { ID = 1, Name = "Rally 2021" },
            };

            foreach (Game item in games)
            {
                context.Games.Add(item);
            }

            context.SaveChanges();
        }

        public static void SeedQuestions(DatabaseContext context)
        {
            if (context.Questions.Any())
            {
                return;
            }

            var questions = new Question[]
            {
                new Question { GameID = 1, Title = "Šiaurės žvaigždė", Type = QuestionType.PHOTO, Image = "https://s3.amazonaws.com/cdn-origin-etr.akc.org/wp-content/uploads/2017/11/26155623/Siberian-Husky-standing-outdoors-in-the-winter.jpg", Text = "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lorem ipsum may be used before final copy is available, but it may also be used to temporarily replace copy in a process called greeking, which allows designers to consider form without the meaning of the text influencing the design." },
            };

            foreach (Question q in questions)
            {
                context.Questions.Add(q);
            }

            context.SaveChanges();
        }

        public static void SeedPlayers(DatabaseContext context)
        {
            if (context.Players.Any())
            {
                return;
            }

            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, "123456");
            var players = new Player[]
            {
                new Player { Name = "Emilis & Co.", Phone = "+37063899324", StartPosition = 21, StartTime = new DateTime(2020, 5, 9, 12, 53, 5), Password = hashedPassword },
            };

            foreach (Player item in players)
            {
                context.Players.Add(item);
            }

            context.SaveChanges();
        }
    }
}