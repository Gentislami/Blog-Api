using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;

namespace Blog_Api.src.Services.Entity;

public interface IApplicationUserEntityService : IEntityService<ApplicationUser, ApplicationUserRepository>;
public class ApplicationUserEntityService(IApplicationUserRepository repository) : AbstractEntityService<ApplicationUser, IApplicationUserRepository>(repository), IApplicationUserEntityService
{
}
