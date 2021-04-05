using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using TrallyRally.Data;
using TrallyRally.Models;
using TrallyRally.Services;
using TrallyRally.Dtos;

namespace TrallyRally.Controllers.API
{
    public class SubmitAnswerRequest
    {
        public string Answer { get; set; }
    }

    [Authorize]
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public QuestionsController(DatabaseContext context, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuestionDto>>> Get()
        {
            var user = _userService.GetUserFromClaims(User);
            var player = await _context.Players
                .Include(player => player.Games)
                .ThenInclude(game => game.Questions)
                .ThenInclude(question => question.QuestionSubmissions.Where(submission => submission.PlayerID == user.ID))
                .FirstOrDefaultAsync();

            var game = player.Games.OrderByDescending(game => game.CreatedDate).FirstOrDefault();

            if (game == null)
            {
                return new List<QuestionDto>();
            }

            var questions = game.Questions.ToList();

            return questions.ConvertAll(q => q.ConvertToDto());
        }

        [HttpPut("{id}/submit")]
        public IActionResult Submit(int id, [FromBody] SubmitAnswerRequest request)
        {
            var user = _userService.GetUserFromClaims(User);
            var question = _context.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            var newAnswer = request.Answer;

            if (question.Type == QuestionType.PHOTO)
            {
                if (string.IsNullOrEmpty(request.Answer))
                {
                    return BadRequest();
                }

                newAnswer = uploadPhoto(request.Answer);
            }

            var submission = _context.QuestionSubmissions.FirstOrDefault(x => x.QuestionID == id && x.PlayerID == user.ID);

            if (submission == null)
            {
                submission = new QuestionSubmission { PlayerID = user.ID, QuestionID = id, Answer = newAnswer };
                _context.QuestionSubmissions.Add(submission);
            }
            else
            {
                submission.Answer = newAnswer;
            }

            _context.SaveChanges();

            return Ok();
        }

        private string uploadPhoto(string base64Photo)
        {
            byte[] photoData = Convert.FromBase64String(base64Photo);

            Image image;
            using (MemoryStream ms = new MemoryStream(photoData))
            {
                image = Image.FromStream(ms);
            }

            var randomFileName = Guid.NewGuid().ToString() + ".jpg";
            var relativePath = Path.Combine("uploads/answers", randomFileName);
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);
            image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return relativePath;
        }
    }
}
