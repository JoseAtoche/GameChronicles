using Core.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System.Threading.Tasks;

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
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId).ConfigureAwait(false);
            var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId).ConfigureAwait(false);

            if (!userExists || !gameExists)
            {
                return null;
            }

            var userGame = new UserGame { UserId = userId, GameId = gameId };
            _context.UserGames.Add(userGame);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return userGame;
        }

        public async Task<UserGame> UnassignGameFromUserAsync(int userId, int gameId)
        {
            var userGame = await _context.UserGames
                .FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GameId == gameId)
                .ConfigureAwait(false);

            if (userGame == null)
            {
                return null;
            }

            _context.UserGames.Remove(userGame);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return userGame;
        }
    }
}
