using API.Models;

namespace API.DTOs.Employees
{
    public class CreateEmployeeDto
    // Representasi DTO untuk membuat entitas Employee
    {
        //properti Employee yang akan dibuat
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Operator implisit untuk mengubah objek CreateEmployeeDto menjadi objek Employee
        public static implicit operator Employee(CreateEmployeeDto createEmployeeDto)
        {
            return new Employee
            {
                Nik = createEmployeeDto.Nik,
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName,
                BirthDate = createEmployeeDto.BirthDate,
                Gender = createEmployeeDto.Gender,
                HiringDate = createEmployeeDto.HiringDate,
                Email = createEmployeeDto.Email,
                PhoneNumber = createEmployeeDto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
