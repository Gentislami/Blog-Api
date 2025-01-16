using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;

namespace Blog_Api.src.Services.Entity;

public interface ILikeEntityService : IEntityService<Like, LikeRepository>;
public class LikeEntityService(ILikeRepository repository) : AbstractEntityService<Like, ILikeRepository>(repository), ILikeEntityService
{
}
