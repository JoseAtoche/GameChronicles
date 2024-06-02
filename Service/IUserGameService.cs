using Core.Models;

namespace Service
{
    public interface IUserGameService
    {
        Task<UserGame> AssignGameToUserAsync(int userId, int gameId);
        Task<UserGame> UnassignGameFromUserAsync(int userId, int gameId);
    }
}
