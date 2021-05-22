using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrallyRally.Data;
using TrallyRally.Models;

namespace TrallyRally.Controllers
{
    public class GamesController : Controller
    {
        private readonly DatabaseContext _context;

        public GamesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Games.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.Include(x => x.Games).ToListAsync();
            var assignedQuestions = questions.Where(q => q.Games.Select(g => g.ID).ToList().Contains(game.ID)).ToList();
            var nonAssignedQuestions = questions.Except(assignedQuestions).ToList();

            var players = await _context.Players.Include(x => x.Games).ToListAsync();
            var assignedPlayers = players.Where(q => q.Games.Select(g => g.ID).ToList().Contains(game.ID)).ToList();
            var nonAssignedPlayers = players.Except(assignedPlayers).ToList();

            ViewData["AssignedQuestions"] = assignedQuestions;
            ViewData["NonAssignedQuestions"] = nonAssignedQuestions;

            ViewData["AssignedPlayers"] = assignedPlayers;
            ViewData["NonAssignedPlayers"] = nonAssignedPlayers;

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.CreatedDate = DateTime.Now;
                game.ModifiedDate = DateTime.Now;

                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,CreatedDate")] Game game)
        {
            if (id != game.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    game.ModifiedDate = DateTime.Now;

                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> QuestionAssign(int gameID, int questionID)
        {
            var game = await _context.Games.Include(x => x.Questions).Where(x => x.ID == gameID).FirstOrDefaultAsync();
            var question = await _context.Questions.FindAsync(questionID);
            if (game == null || question == null)
            {
                return NotFound();
            }

            game.Questions.Add(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = gameID });
        }

        public async Task<IActionResult> QuestionUnassign(int gameID, int questionID)
        {
            var game = await _context.Games.Include(x => x.Questions).Where(x => x.ID == gameID).FirstOrDefaultAsync();
            var question = await _context.Questions.FindAsync(questionID);
            if (game == null || question == null)
            {
                return NotFound();
            }

            game.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = gameID });
        }

        public async Task<IActionResult> PlayerAssign(int gameID, int playerID)
        {
            var game = await _context.Games.Include(x => x.Players).Where(x => x.ID == gameID).FirstOrDefaultAsync();
            var player = await _context.Players.FindAsync(playerID);
            if (game == null || player == null)
            {
                return NotFound();
            }

            game.Players.Add(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = gameID });
        }

        public async Task<IActionResult> PlayerUnassign(int gameID, int playerID)
        {
            var game = await _context.Games.Include(x => x.Players).Where(x => x.ID == gameID).FirstOrDefaultAsync();
            var player = await _context.Players.FindAsync(playerID);
            if (game == null || player == null)
            {
                return NotFound();
            }

            game.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = gameID });
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.ID == id);
        }
    }
}
