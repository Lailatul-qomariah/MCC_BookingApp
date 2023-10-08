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
    public AccountRepository(BookingManagementDBContext context) : base(context)
    {
       
    }

    public Account GetByEmail(string email)
    {
        // Menggunakan LINQ untuk mencari data pertama di dalam tabel Accounts
        // di mana alamat emailnya yang terkait cocok dengan alamat email yang diberikan.
        var account = _context.Accounts
                .FirstOrDefault(account => account.Employee.Email == email);

        return account;
    }


}

