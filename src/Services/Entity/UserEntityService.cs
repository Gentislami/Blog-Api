using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;

namespace Blog_Api.src.Services.Entity;

public interface IUserEntityService : IEntityService<ApplicationUser, UserRepository>;
public class UserEntityService(IUserRepository repository) : AbstractEntityService<ApplicationUser, IUserRepository>(repository), IUserEntityService
{
}
