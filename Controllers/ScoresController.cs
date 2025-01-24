using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaderBoard.Data;
using LeaderBoard.Model;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;


namespace LeaderBoard.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _redisDb;

        public ScoresController(ApplicationDbContext context, IConnectionMultiplexer redisConnection)
        {
            _context = context;
            _redisConnection = redisConnection;
            _redisDb = _redisConnection.GetDatabase();
        }

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScores()
        {
            return await _context.Scores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(int id)
        {
            var score = await _context.Scores.FindAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScore(int id, Score score)
        {
            if (id != score.id)
            {
                return BadRequest();
            }

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

      
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
            var userId = score.user_id;
            var gameKey = $"leaderboard:{score.game_id}";

            var added = await _redisDb.SortedSetAddAsync(gameKey, userId, score.score);
            _context.Scores.Add(score);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScore", new { id = score.id }, score);
        }
        [HttpGet("leaderboard/{gameId}")]
        public async Task<IActionResult> GetLeaderboard(string gameId)
        {
            var gameKey = $"leaderboard:{gameId}";
 
            var topPlayers = await _redisDb.SortedSetRangeByRankWithScoresAsync(gameKey, 0, 9, Order.Descending);

            var leaderboard = new List<LeaderboardEntry>();
            foreach (var player in topPlayers)
            {
                leaderboard.Add(new LeaderboardEntry
                {
                    UserId = player.Element,
                    Score = player.Score
                });
            }

            return Ok(leaderboard);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            var gameKey = $"leaderboard:{score.game_id}";
            await _redisDb.SortedSetRemoveAsync(gameKey, score.user_id);

            _context.Scores.Remove(score);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScoreExists(int id)
        {
            return _context.Scores.Any(e => e.id == id);
        }
    }
    public class LeaderboardEntry
    {
        public string UserId { get; set; }
        public double Score { get; set; }
    }
}
