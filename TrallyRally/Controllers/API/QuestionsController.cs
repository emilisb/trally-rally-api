using System;
using System.IO;
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
using TrallyRally.Helpers;

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
        public async Task<ActionResult<List<QuestionDto>>> Get(double latitude, double longitude)
        {
            var user = _userService.GetUserFromClaims(User);
            var player = GetPlayerWithQuestionsAndSubmissions(user.ID);

            var game = player.Games.OrderByDescending(game => game.CreatedDate).FirstOrDefault();

            if (game == null)
            {
                return new List<QuestionDto>();
            }

            var questionDtos = game.Questions.ToList().ConvertAll(q => q.ConvertToDto());
            var questionsWithLockedDetails = HideLockedQuestionDetails(questionDtos);

            return questionsWithLockedDetails;
        }

        [HttpPut("{id}/submit")]
        public IActionResult Submit(int id, [FromBody] SubmitAnswerRequest request)
        {
            var user = _userService.GetUserFromClaims(User);
            var player = GetPlayerWithQuestionsAndSubmissions(user.ID);
            var game = player.Games.OrderByDescending(game => game.CreatedDate).FirstOrDefault();

            var question = game.Questions.FirstOrDefault(q => q.ID == id);
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

                newAnswer = UploadPhoto(request.Answer);
            }

            var submission = _context.QuestionSubmissions.FirstOrDefault(x => x.QuestionID == id && x.PlayerID == user.ID);

            if (submission == null)
            {
                submission = new QuestionSubmission { PlayerID = user.ID, QuestionID = id, GameID = game.ID, Answer = newAnswer, Correct = IsAnswerCorrect(question, newAnswer) };
                _context.QuestionSubmissions.Add(submission);
            }
            else
            {
                submission.Answer = newAnswer;
                submission.Correct = IsAnswerCorrect(question, newAnswer);
            }

            _context.SaveChanges();

            return Ok(new { Success = true });
        }

        private Player GetPlayerWithQuestionsAndSubmissions(int id)
        {
            return _context.Players
                .Where(player => player.ID == id)
                .Include(player => player.Games)
                .ThenInclude(game => game.Questions)
                .ThenInclude(question => question.QuestionSubmissions.Where(submission => submission.PlayerID == id))
                .FirstOrDefault();
        }

        private string UploadPhoto(string base64Photo)
        {
            return "";
        }

        private bool? IsAnswerCorrect(Question question, string newAnswer)
        {
            var questionHasAnswer = !String.IsNullOrEmpty(question.Answer);

            if (questionHasAnswer)
            {
                var answersMatch = question.Answer.ToLower() == newAnswer.ToLower();
                if (question.Type == QuestionType.QR || answersMatch)
                {
                    return answersMatch;
                }
            }

            // Set correct status to null so administrator would have to correct answer again
            // (in case of player guessing the correct answer and then changing the answer to wrong)
            return null;
        }

        private List<QuestionDto> HideLockedQuestionDetails(IList<QuestionDto> questionDtos)
        {
            var index = 0;
            var updatedQuestions = new List<QuestionDto>(questionDtos);
            foreach (var question in updatedQuestions)
            {
                bool locked = question.MaxDistance < 0;
                question.Locked = locked;

                if (locked)
                {
                    question.Title = $"-Užrakintas Klausimas {index + 1}-";
                    question.Text = "";
                }

                index++;
            }

            return updatedQuestions;
        }
    }
}
