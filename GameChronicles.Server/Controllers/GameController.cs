using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Domain.Interfaces;

namespace GameChronicles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/Game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            var games = await _gameService.GetAllAsync();
            return Ok(games);
        }

        // GET: api/Game/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> Get(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return game;
        }

        // POST: api/Game
        [HttpPost]
        public async Task<ActionResult<Game>> Post([FromBody] Game game)
        {
            var createdGame = await _gameService.CreateAsync(game);
            return CreatedAtAction(nameof(Get), new { id = createdGame.Id }, createdGame);
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            var updated = await _gameService.UpdateAsync(game);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Game/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _gameService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
