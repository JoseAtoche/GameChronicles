using Core.Models;

namespace Domain.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<List<Game>> GetGamesByPlatformAsync(string platform);
        Task<List<Game>> GetGamesByGenreAsync(string genre);
    }
}
