using Core.Models;
using Domain.Interfaces;
using Service.Interfaces;

namespace Service.Implementations
{
    public class UserGameService : IUserGameService
    {
        private readonly IUserGameRepository _userGameRepository;

        public UserGameService(IUserGameRepository userGameRepository)
        {
            _userGameRepository = userGameRepository;
        }

        public async Task<UserGame> AssignGameToUserAsync(int userId, int gameId)
        {
            return await _userGameRepository.AssignGameToUserAsync(userId, gameId);
        }

        public async Task<UserGame> UnassignGameFromUserAsync(int userId, int gameId)
        {
            return await _userGameRepository.UnassignGameFromUserAsync(userId, gameId);
        }

    }
}
