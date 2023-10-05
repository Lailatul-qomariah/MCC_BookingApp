using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRepository : AllRepositoryGeneric<Account>, IAccountRepository
//inheritance pada genericrepository dan interface repository
{
    private readonly BookingManagementDBContext _context;
    //injection dbcontext
    public AccountRepository(BookingManagementDBContext context) : base(context) 
    { 
        _context = context;
    }

    /* public Account GetByEmail(string email)
     {
         return _context.Accounts.FirstOrDefault(a => a.Email == email);
     }*/

    public Account GetByEmail(string email)
{
        var account = _context.Accounts
                      .Join(
                          _context.Employees,
                          account => account.Guid,
                          employee => employee.Guid,
                          (account, employee) => new
                          {
                              Account = account,
                              Employee = employee
                          }
                      )
                      .Where(joinResult => joinResult.Employee.Email == email)
                      .Select(joinResult => joinResult.Account)
                      .FirstOrDefault();

        return account;
    }


}

