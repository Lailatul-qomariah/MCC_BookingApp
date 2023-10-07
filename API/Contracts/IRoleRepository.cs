using API.Models;
using API.Repositories;

namespace API.Contracts;

public interface IRoleRepository : IGenericRepository<Role>
{
    public Guid? GetDefaultRoleGuid();

}
