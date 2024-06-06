using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Service;

namespace GameChronicles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGameController : ControllerBase
    {
        private readonly IUserGameService _userGameService;

        public UserGameController(IUserGameService userGameService)
        {
            _userGameService = userGameService;
        }

        // POST: api/UserGame
        [HttpPost]
        public async Task<ActionResult<UserGame>> AddUserGame([FromBody] UserGame userGame)
        {
            try
            {

                var addedUserGame = await _userGameService.AssignGameToUserAsync(userGame.UserId, userGame.GameId);
                return Ok(addedUserGame);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE
        [HttpDelete("{userId}/{gameId}")]
        public async Task<IActionResult> UnassignGameFromUser(int userId, int gameId)
        {
            var userGame = await _userGameService.UnassignGameFromUserAsync(userId, gameId);
            if (userGame == null)
            {
                return NotFound(); 
            }
            return NoContent();
        }

    }
}
