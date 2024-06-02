using Core.Models;
using System;

namespace Domain.Interfaces
{
    public interface IUserGameRepository
    {
        Task<UserGame> AssignGameToUserAsync(int userId, int gameId);
        Task<UserGame> UnassignGameFromUserAsync(int userId, int gameId);
    }
}
