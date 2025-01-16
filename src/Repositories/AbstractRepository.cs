using Blog_Api.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(string id);
        TEntity? GetById(string id);
        Task<int> AddAsync(TEntity entity);
        void Add(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        void Update(TEntity entity);
        Task<int> DeleteAsync(string id);
        void Delete(string id);
        bool Exists(string id);
    }

    public abstract class AbstractRepository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public TEntity? GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public void Delete(string id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                entity.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
        }

        public bool Exists(string id)
        {
            return _dbSet.Any(entity => entity.Id == id);
        }
    }
}
