using API.Models;

namespace API.DTOs.AccountRoles;

public class CreateAccountRoleDto
// Representasi DTO untuk membuat entitas AccountRole
{
    //properti AccountRole yang akan dibuat
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }

    // Operator implisit untuk mengubah objek CreateAccountRoleDto menjadi objek AccountRole
    public static implicit operator AccountRole(CreateAccountRoleDto accRoleDto)
    {
        // Inisiasi objek AccountRole dengan data dari objek CreateAccountRoleDto
        return new AccountRole
        {
            AccountGuid = accRoleDto.AccountGuid,
            RoleGuid = accRoleDto.RoleGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
