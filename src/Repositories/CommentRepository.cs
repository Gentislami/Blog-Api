using Blog_Api.src.Data;
using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByBlogPostIdAsync(int blogPostId);
        Task<IEnumerable<Comment>> GetByUserIdAsync(int userId); // Add this method
    }
    public class CommentRepository : AbstractRepository<Comment>, ICommentRepository
    {
        public CommentRepository(Blog_ApiContext context) : base(context) { }

        public async Task<IEnumerable<Comment>> GetByBlogPostIdAsync(int blogPostId)
        {
            return await _context.Set<Comment>().Where(c => c.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetByUserIdAsync(int userId)
        {
            return await _context.Set<Comment>().Where(c => c.UserId == userId).ToListAsync();
        }
    }

}
