using Core.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
