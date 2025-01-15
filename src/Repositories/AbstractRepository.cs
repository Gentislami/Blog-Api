using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public abstract class AbstractRepository<T>(DbContext context) : IRepository<T> where T : AbstractEntity
    {
        protected readonly DbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
