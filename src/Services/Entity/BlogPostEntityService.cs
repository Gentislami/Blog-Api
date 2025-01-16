using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;

namespace Blog_Api.src.Services.Entity;

public interface IBlogPostEntityService : IEntityService<BlogPost, BlogPostRepository>;
public class BlogPostEntityService(IBlogPostRepository repository) : AbstractEntityService<BlogPost, IBlogPostRepository>(repository), IBlogPostEntityService
{
}
