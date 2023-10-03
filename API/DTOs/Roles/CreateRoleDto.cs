using API.Models;

namespace API.DTOs.Roles
{
    public class CreateRoleDto
    // Representasi DTO untuk membuat entitas Role
    {
        //properti Role yang akan dibuat
        public string Name { get; set; }

        // Operator implisit untuk mengubah objek CreateRoleDto menjadi Role
        public static implicit operator Role(CreateRoleDto createRolesDto)
        {
            // Inisiasi objek Role dengan data dari objek CreateRoleDto
            return new Role
            {
                Name = createRolesDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
