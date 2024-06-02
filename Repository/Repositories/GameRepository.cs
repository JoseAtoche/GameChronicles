using Core.Models;
using Domain.Interfaces;
using Repository.Data;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Games.ToListAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task<Game> CreateAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<bool> UpdateAsync(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return false;

            _context.Games.Remove(game);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Game>> GetGamesByPlatformAsync(string platform)
        {
            return await _context.Games.Where(g => g.Platform == platform).ToListAsync();
        }

        public async Task<List<Game>> GetGamesByGenreAsync(string genre)
        {
            return await _context.Games.Where(g => g.Genre == genre).ToListAsync();
        }
    }
}
