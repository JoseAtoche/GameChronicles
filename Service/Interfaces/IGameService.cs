using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IGameService : IGenericService<Game>
    {
        Task<List<Game>> GetGamesByPlatformAsync(string platform);
        Task<List<Game>> GetGamesByGenreAsync(string genre);
    }
}
