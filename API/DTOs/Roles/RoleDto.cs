using API.Models;

namespace API.DTOs.Roles
{
    public class RoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public static explicit operator RoleDto(Role role)
        {
            return new RoleDto
            {
                Guid = role.Guid,
                Name = role.Name
            };
        }

        public static implicit operator Role(RoleDto rolesDto)
        {
            return new Role
            {
                Guid = rolesDto.Guid,
                Name = rolesDto.Name,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
