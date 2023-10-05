using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EmployeeRepository : AllRepositoryGeneric<Employee>, IEmployeeRepository
//inharitance pada genericrepository dan interface repository
{

    //injection dbcontect
    private readonly BookingManagementDBContext _context;
    public EmployeeRepository(BookingManagementDBContext context) : base(context)
    {
        _context = context;
    }

    public Employee GetLastNik() //method untuk find last nik
    {
        return _context.Employees
            .OrderByDescending(e => e.Nik) //urutkan descending
            .FirstOrDefault();

    }
    
}