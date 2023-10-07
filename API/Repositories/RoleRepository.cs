using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class RoleRepository : AllRepositoryGeneric<Role>, IRoleRepository
//inheritance pada genericrepository dan interface repository
{
    //injection dbcontext
    public RoleRepository(BookingManagementDBContext context) : base(context) {
       
    }

    public Guid? GetDefaultRoleGuid()
    {
        return _context.Set<Role>().FirstOrDefault(r => r.Name == "user")?.Guid;
    }

}