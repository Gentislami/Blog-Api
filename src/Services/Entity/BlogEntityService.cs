using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;

namespace Blog_Api.src.Services.Entity;

public interface IBlogEntityService : IEntityService<Blog, BlogRepository>;
public class BlogEntityService(IBlogRepository repository) : AbstractEntityService<Blog, IBlogRepository>(repository), IBlogEntityService
{
}
