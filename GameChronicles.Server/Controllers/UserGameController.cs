using Microsoft.AspNetCore.Mvc;
using Core.Models;
using System;
using System.Threading.Tasks;
using Service.Interfaces;
using Service.Factories;

namespace GameChronicles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGameController : ControllerBase
    {
        private readonly IUserGameService _userGameService;

        public UserGameController(ServiceFactory serviceFactory)
        {
            _userGameService = serviceFactory.CreateUserGameService();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserGame([FromBody] UserGame userGame)
        {
            if (userGame == null)
            {
                return BadRequest("UserGame object is null");
            }

            try
            {
                var addedUserGame = await _userGameService.AssignGameToUserAsync(userGame.UserId, userGame.GameId);
                return Ok(addedUserGame);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("{userId}/{gameId}")]
        public async Task<IActionResult> UnassignGameFromUser(int userId, int gameId)
        {
            try
            {
                var result = await _userGameService.UnassignGameFromUserAsync(userId, gameId);
                if (result == null)
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
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
