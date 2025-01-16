using Blog_Api.src.Data;
using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetBlogsByUserIdAsync(string userId);
    }

    public class BlogRepository(Blog_ApiContext context) : AbstractRepository<Blog>(context), IBlogRepository
    {
        public async Task<IEnumerable<Blog>> GetBlogsByUserIdAsync(string userId)
        {
            return await _dbSet.Where(b => b.User.Id == userId).ToListAsync();
        }
    }

}
