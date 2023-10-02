using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRoleRepository : AllRepositoryGeneric<AccountRole>, IAccountRoleRepository

{
    public AccountRoleRepository(BookingManagementDBContext context) : base(context) { }

   /* private readonly BookingManagementDBContext _context;

    public AccountRoleRepository(BookingManagementDBContext context)
    {
        _context = context;
    }
    public AccountRole? Create(AccountRole accountRole)
    {
        try
        {
            _context.Set<AccountRole>().Add(accountRole);
            _context.SaveChanges();
            return accountRole;
        }catch
        {
            return null;
        }
    }

    public bool Delete(AccountRole accountRole)
    {
        try
        {
            _context.Set<AccountRole>().Remove(accountRole);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<AccountRole> GetAll()
    {
        return _context.Set<AccountRole>().ToList();
    }

    public AccountRole? GetByGuid(Guid guid)
    {
        return _context.Set<AccountRole>().Find(guid);
    }

    public bool Update(AccountRole accountRole)
    {
        try
        {
            _context.Set<AccountRole>().Update(accountRole);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}*/
}