using Blog_Api.src.Data;
using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        Task<IEnumerable<BlogPost>> GetBlogPostsByBlogIdAsync(int blogId);
        Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(int userId);  // New method
    }

    public class BlogPostRepository : AbstractRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(Blog_ApiContext context) : base(context) { }

        public async Task<IEnumerable<BlogPost>> GetBlogPostsByBlogIdAsync(int blogId)
        {
            return await _context.Set<BlogPost>()
                                 .Where(p => p.BlogId == blogId)
                                 .ToListAsync();
        }

        // New method to get all blog posts by user
        public async Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(int userId)
        {
            return await _context.Set<BlogPost>()
                                 .Where(p => p.UserId == userId)  // Get posts by the user
                                 .ToListAsync();
        }
    }
}
