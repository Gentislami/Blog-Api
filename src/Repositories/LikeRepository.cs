using Blog_Api.src.Data;
using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public interface ILikeRepository : IRepository<Like>
    {
        Task<IEnumerable<Like>> GetByBlogPostIdAsync(string blogPostId);
    }

    public class LikeRepository(Blog_ApiContext context) : AbstractRepository<Like>(context), ILikeRepository
    {
        public async Task<IEnumerable<Like>> GetByBlogPostIdAsync(string blogPostId)
        {
            return await _dbSet.Where(l => l.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
