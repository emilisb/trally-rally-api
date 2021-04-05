using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TrallyRally.Data;
using TrallyRally.Models;
using TrallyRally.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrallyRally.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IUserService _userService;

        public QuestionsController(DatabaseContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<List<Question>>> Get()
        {
            var user = _userService.GetUserFromClaims(User);
            var player = await _context.Players.Include(x => x.Games).ThenInclude(x => x.Questions).FirstOrDefaultAsync();
            var game = player.Games.OrderByDescending(game => game.CreatedDate).FirstOrDefault();

            if (game == null)
            {
                return new List<Question>();
            }

            return game.Questions.ToList();
        }

        // GET api/values/5

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
