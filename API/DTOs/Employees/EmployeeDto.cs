using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class EmployeeDto
    //representasi DTO untuk model atau entitaas Employee
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevels Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Operator eksplisit untuk convert objek Employee ke EmployeeDto
        //digunakan atau dipanggil pada method GetAll, GetByGuid dan Create
        public static explicit operator EmployeeDto(Employee employee)
        {
            // Inisiasi objek EmployeeDto dengan data dari objek Employee
            return new EmployeeDto
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            };
        }

        // Operator implisit untuk convert objek EmployeeDto ke objek Employee
        //digunakan pada saat menggunakan method Update
        public static implicit operator Employee(EmployeeDto employeeDto)
        {
            // Inisiasi objek Employee dengan data dari objek EmployeeDto
            return new Employee
            {
                Guid = employeeDto.Guid,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                Gender = employeeDto.Gender,
                HiringDate = employeeDto.HiringDate,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
