using API.Models;

namespace API.DTOs.AccountRoles
{
    public class AccountRoleDto
    {
        //representasi DTO untuk model atau entitaas AccountRole
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        // Operator eksplisit untuk convert objek AccountRole ke AccountRoleDto
        //digunakan atau dipanggil pada method GetAll, GetByGuid dan Create di controller
        public static explicit operator AccountRoleDto(AccountRole accRoleDto)
        {
            // Inisiasi objek UniversityDto dengan data dari objek AccountRole
            return new AccountRoleDto
            {
                Guid = accRoleDto.Guid,
                AccountGuid = accRoleDto.AccountGuid,
                RoleGuid = accRoleDto.RoleGuid
            };
        }

        // Operator implisit untuk convert objek AccountRoleDto ke AccountRole
        //digunakan pada saat menggunakan method Update di controller
        public static implicit operator AccountRole(AccountRoleDto accRoleDto)
        {
            // Inisiasi objek AccountRole dengan data dari objek AccountRoleDto
            return new AccountRole
            {
                Guid = accRoleDto.Guid,
                AccountGuid = accRoleDto.AccountGuid,
                RoleGuid = accRoleDto.RoleGuid
            };
        }
    }
}
