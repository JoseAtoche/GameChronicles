using Core.Models;
using Domain.Interfaces;
using Service.Interfaces;

namespace Service.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game> CreateAsync(Game game)
        {
            return await _gameRepository.CreateAsync(game);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _gameRepository.DeleteAsync(id);
        }

        public async Task<List<Game>> GetAllAsync()
        {
            return await _gameRepository.GetAllAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _gameRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Game game)
        {
            return await _gameRepository.UpdateAsync(game);
        }

        public async Task<List<Game>> GetGamesByPlatformAsync(string platform)
        {
            return await _gameRepository.GetGamesByPlatformAsync(platform);
        }

        public async Task<List<Game>> GetGamesByGenreAsync(string genre)
        {
            return await _gameRepository.GetGamesByGenreAsync(genre);
        }
    }
}
