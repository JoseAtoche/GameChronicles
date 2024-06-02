using Core.Models;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGameService : IGenericService<Game>
    {
        Task<List<Game>> GetGamesByPlatformAsync(string platform);
        Task<List<Game>> GetGamesByGenreAsync(string genre);
    }
}
