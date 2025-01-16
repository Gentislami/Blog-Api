using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;

namespace Blog_Api.src.Services.Entity;

public interface ICommentEntityService : IEntityService<Comment, CommentRepository>;
public class CommentEntityService(ICommentRepository repository) : AbstractEntityService<Comment, ICommentRepository>(repository), ICommentEntityService
{
}
