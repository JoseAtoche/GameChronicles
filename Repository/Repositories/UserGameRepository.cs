using Core.Models;
using Repository.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;

namespace Repository.Repositories
{
    public class UserGameRepository : IUserGameRepository
    {
        private readonly GameChroniclesDbContext _context;

        public UserGameRepository(GameChroniclesDbContext context)
        {
            _context = context;
        }

        public async Task<UserGame> AssignGameToUserAsync(int userId, int gameId)
        {
            var userGame = new UserGame { UserId = userId, GameId = gameId };
            _context.UserGames.Add(userGame);
            await _context.SaveChangesAsync();
            return userGame;
        }

        public async Task<UserGame> UnassignGameFromUserAsync(int userId, int gameId)
        {
            var userGame = await _context.UserGames.FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GameId == gameId);
            if (userGame != null)
            {
                _context.UserGames.Remove(userGame);
                await _context.SaveChangesAsync();
            }
            return userGame;
        }
    }
}
