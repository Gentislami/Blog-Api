using Blog_Api.src.Data;
using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }

    public class UserRepository : AbstractRepository<User>, IUserRepository
    {
        public UserRepository(Blog_ApiContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>()
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
