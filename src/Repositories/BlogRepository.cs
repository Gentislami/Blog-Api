using Blog_Api.src.Data;
using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetBlogsByUserIdAsync(int userId);
    }

    public class BlogRepository : AbstractRepository<Blog>, IBlogRepository
    {
        public BlogRepository(Blog_ApiContext context) : base(context) { }

        public async Task<IEnumerable<Blog>> GetBlogsByUserIdAsync(int userId)
        {
            return await _context.Set<Blog>()
                                 .Where(b => b.UserId == userId)
                                 .ToListAsync();
        }
    }

}
