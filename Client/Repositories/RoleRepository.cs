using API.DTOs.Employees;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Client.Repositories;
[Authorize (Roles="admin")]

public class RoleRepository : GeneralRepository<Role, Guid>, IRoleRepository
{
    public RoleRepository(string request = "Role/") : base(request)
    {
    }



}
