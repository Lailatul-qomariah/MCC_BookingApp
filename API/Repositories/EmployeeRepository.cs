using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EmployeeRepository : AllRepositoryGeneric<Employee>, IEmployeeRepository
//inharitance pada genericrepository dan interface repository
{

    //injection dbcontect
    public EmployeeRepository(BookingManagementDBContext context) : base(context)
    {
    }

    public Employee GetLastNik() //method untuk find last nik
    {
        return _context.Employees
            .OrderByDescending(e => e.Nik) //urutkan descending
            .FirstOrDefault();

    }
    public Employee GetByEmail(string email)
    {
        // Menggunakan LINQ untuk mencari data pertama di dalam tabel employee
        // di mana alamat emailnya yang terkait cocok dengan alamat email yang diberikan.
        var employee = _context.Employees
                .FirstOrDefault(emp => emp.Email == email);

        return employee;
    }

}