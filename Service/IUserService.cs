using Core.Models;
using Domain.Interfaces;

namespace Service
{
    public interface IUserService :IGenericService<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}