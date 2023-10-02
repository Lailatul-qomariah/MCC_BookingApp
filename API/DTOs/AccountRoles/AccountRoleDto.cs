using API.Models;

namespace API.DTOs.AccountRoles
{
    public class AccountRoleDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        // Konversi Eksplisit (Explicit Conversion):
        // Metode ini akan mengonversi EmployeeDto ke Employee secara eksplisit jika diperlukan.
        public static explicit operator AccountRoleDto(AccountRole accRoleDto)
        {
            return new AccountRoleDto
            {
                Guid = accRoleDto.Guid,
                AccountGuid = accRoleDto.AccountGuid,
                RoleGuid = accRoleDto.RoleGuid
            };
        }

        // Konversi Implisit (Implicit Conversion):
        // Metode ini akan mengonversi EmployeeDto ke Employee secara implisit jika diperlukan.
        public static implicit operator AccountRole(AccountRoleDto accRoleDto)
        {
            return new AccountRole
            {
                Guid = accRoleDto.Guid,
                AccountGuid = accRoleDto.AccountGuid,
                RoleGuid = accRoleDto.RoleGuid
            };
        }
    }
}
