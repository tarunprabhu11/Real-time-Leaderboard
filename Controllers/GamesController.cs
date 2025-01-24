using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaderBoard.Data;
using LeaderBoard.Model;
using Microsoft.AspNetCore.Authorization;

namespace LeaderBoard.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Games>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Games>> GetGames(int id)
        {
            var games = await _context.Games.FindAsync(id);

            if (games == null)
            {
                return NotFound();
            }

            return games;
        }


        [HttpPost("/add")]
        public async Task<ActionResult<Games>> PostGames(Games games)
        {
            _context.Games.Add(games);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGames", new { id = games.id }, games);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGames(int id)
        {
            var games = await _context.Games.FindAsync(id);
            if (games == null)
            {
                return NotFound();
            }

            _context.Games.Remove(games);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
