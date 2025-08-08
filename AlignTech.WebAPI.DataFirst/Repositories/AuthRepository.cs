using AlignTech.WebAPI.DataFirst.Data;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AlignTech.WebAPI.DataFirst.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly QuickKartDbContext _dbContext;

        public AuthRepository(QuickKartDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> LoginAsync(string username)
        {
            var result = await _dbContext.UserTable.SingleOrDefaultAsync(u => u.Username == username);
            return result ?? null!;
        }

        public async Task<User> RegisterAsync(User user)
        {
            var result = await _dbContext.UserTable.AnyAsync(u => u.Username == user.Username);
            if (result)
            {
                return null!;
            }
            _dbContext.UserTable.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
