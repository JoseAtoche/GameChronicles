using Core.Models;

namespace Service.Interfaces
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}