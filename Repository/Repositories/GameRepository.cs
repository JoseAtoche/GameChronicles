using Core.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameChroniclesDbContext _context;

        public GameRepository(GameChroniclesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> GetAllAsync()
        {
            return await _context.Games.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<Game> CreateAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return game;
        }

        public async Task<bool> UpdateAsync(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gameExists = await _context.Games.AnyAsync(g => g.Id == id).ConfigureAwait(false);
            if (!gameExists)
                return false;

            var game = await _context.Games.FindAsync(id).ConfigureAwait(false);
            if (game == null)
                return false;

            _context.Games.Remove(game);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<List<Game>> GetGamesByPlatformAsync(string platform)
        {
            return await _context.Games
                .Where(g => g.Platform == platform)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<List<Game>> GetGamesByGenreAsync(string genre)
        {
            return await _context.Games
                .Where(g => g.Genre == genre)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
