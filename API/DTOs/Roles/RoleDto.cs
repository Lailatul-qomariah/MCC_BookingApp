using API.Models;

namespace API.DTOs.Roles
{
    //representasi DTO untuk model atau entitaas Role
    public class RoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        // Operator eksplisit untuk convert objek Role ke RoleDto
        //digunakan atau dipanggil pada method GetAll, GetByGuid dan Create
        public static explicit operator RoleDto(Role role)
        {
            // Inisiasi objek RoleDto dengan data dari objek Role
            return new RoleDto
            {
                Guid = role.Guid,
                Name = role.Name
            };
        }
        // Operator implisit untuk convert objek RoleDto ke Role
        //digunakan pada saat menggunakan method Update
        public static implicit operator Role(RoleDto rolesDto)
        {
            // Inisiasi objek Role dengan data dari objek RoleDto
            return new Role
            {
                Guid = rolesDto.Guid,
                Name = rolesDto.Name,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
