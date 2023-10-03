using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EmployeeRepository : AllRepositoryGeneric<Employee>, IEmployeeRepository
//inharitance pada genericrepository dan interface repository
{
    //injection dbcontect
    public EmployeeRepository(BookingManagementDBContext context) : base(context) { }

}