using TrallyRally.Models;
using System;
using System.Linq;

namespace TrallyRally.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any questions.
            if (context.Questions.Any())
            {
                return;   // DB has been seeded
            }

            var questions = new Question[]
            {
                new Question { Title = "Šiaurės žvaigždė", Type = QuestionType.PHOTO, Image = "https://s3.amazonaws.com/cdn-origin-etr.akc.org/wp-content/uploads/2017/11/26155623/Siberian-Husky-standing-outdoors-in-the-winter.jpg", Text = "In publishing and graphic design, Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content. Lorem ipsum may be used before final copy is available, but it may also be used to temporarily replace copy in a process called greeking, which allows designers to consider form without the meaning of the text influencing the design." },
            };

            foreach (Question q in questions)
            {
                context.Questions.Add(q);
            }
            context.SaveChanges();
        }
    }
}