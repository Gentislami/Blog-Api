using Blog_Api.src.Data;
using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByBlogPostIdAsync(string blogPostId);
        Task<IEnumerable<Comment>> GetByUserIdAsync(string userId);
    }
    public class CommentRepository(Blog_ApiContext context) : AbstractRepository<Comment>(context), ICommentRepository
    {
        public async Task<IEnumerable<Comment>> GetByBlogPostIdAsync(string blogPostId)
        {
            return await _dbSet.Where(c => c.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetByUserIdAsync(string userId)
        {
            return await _dbSet.Where(c => c.User.Id == userId).ToListAsync();
        }
    }

}
