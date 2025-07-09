using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.src.Services.Entity;

public interface IEntityService<TEntity, TRepository> where TEntity : class, IEntity where TRepository : class, IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<ActionResult<IEnumerable<TEntity>>> GetAllAsyncAsActionResult();
    Task<TEntity?> GetByIdAsync(string id);
    Task<int> CreateAsync(TEntity entity);
    Task<int> UpdateAsync(TEntity entity);
    Task<int> DeleteAsync(string id);
    bool Exists(string id);
    TEntity createNewObject();
}

public abstract class AbstractEntityService<TEntity, TRepository>(TRepository repository) : IEntityService<TEntity, TRepository> where TEntity : class, IEntity where TRepository : class, IRepository<TEntity>
{
    protected TRepository _repository = repository;

    public void SetRepository(TRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ActionResult<IEnumerable<TEntity>>> GetAllAsyncAsActionResult()
    {
        var result = await _repository.GetAllAsync();
        return result.ToList();
    }

    public async Task<TEntity?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<int> CreateAsync(TEntity entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        return await _repository.UpdateAsync(entity);
    }

    public async Task<int> DeleteAsync(string id)
    {
        return await _repository.DeleteAsync(id);
    }

    public bool Exists(string id)
    {
        return _repository.Exists(id);
    }

    public virtual TEntity createNewObject()
    {
        throw new NotImplementedException(); // This is a fun little thing I like to call 'laziness'.
    }
}