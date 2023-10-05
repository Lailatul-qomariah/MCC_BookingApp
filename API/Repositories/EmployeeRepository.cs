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

    public Employee GetLastNik()
    {
        return _context.Employees
            .OrderByDescending(e => e.Nik)
            .FirstOrDefault();

    }

    public string GetByEmail(string email)
    {
        var findEmail = _context.Employees
            .FirstOrDefault(a => a.Email == email);

        if (findEmail != null)
        {
            return findEmail.Email;
        }

        return null; 
    }

    
}