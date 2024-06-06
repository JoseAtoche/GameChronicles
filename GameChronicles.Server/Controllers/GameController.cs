using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Factories;
using Serilog;

namespace GameChronicles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(ServiceFactory serviceFactory)
        {
            _gameService = serviceFactory.CreateGameService();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            try
            {
                var games = await _gameService.GetAllAsync();
                return Ok(games);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            try
            {
                var game = await _gameService.GetByIdAsync(id);
                if (game == null)
                {
                    return NotFound();
                }
                return Ok(game);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest("Game object is null");
            }

            try
            {
                var createdGame = await _gameService.CreateAsync(game);
                return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, createdGame);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] Game game)
        {
            if (game == null || id != game.Id)
            {
                return BadRequest("Game is null or ID mismatch");
            }

            try
            {
                var updated = await _gameService.UpdateAsync(game);
                if (!updated)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                var deleted = await _gameService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            Log.Error(ex, "Error en el controlador GameController");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
