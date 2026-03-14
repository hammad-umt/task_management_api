using Microsoft.EntityFrameworkCore;
using TMIApi.Data;
using TMIApi.Models;

namespace TMIApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var foundUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (foundUser == null)
                throw new Exception("User nahi mila");

            foundUser.Name = user.Name;
            foundUser.Email = user.Email;
            foundUser.Password = user.Password;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            var foundUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (foundUser == null)
                throw new Exception("User nahi mila");

            _db.Users.Remove(foundUser);
            await _db.SaveChangesAsync();
        }
    }
}
