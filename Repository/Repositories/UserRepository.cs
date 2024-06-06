using Core.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GameChroniclesDbContext _context;

        public UserRepository(GameChroniclesDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync().ConfigureAwait(false);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == id).ConfigureAwait(false);
            if (!userExists)
                return false;

            var user = await _context.Users.FindAsync(id).ConfigureAwait(false);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
        }
    }
}
