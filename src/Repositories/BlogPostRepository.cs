using Blog_Api.src.Data;
using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        Task<IEnumerable<BlogPost>> GetBlogPostsByBlogIdAsync(string blogId);
        Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(string userId);
    }

    public class BlogPostRepository(Blog_ApiContext context) : AbstractRepository<BlogPost>(context), IBlogPostRepository
    {
        public async Task<IEnumerable<BlogPost>> GetBlogPostsByBlogIdAsync(string blogId)
        {
            return await _dbSet.Where(bp => bp.BlogId == blogId).ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(string userId)
        {
            return await _dbSet.Where(bp => bp.User.Id == userId).ToListAsync();
        }
    }
}
