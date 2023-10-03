using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRepository : AllRepositoryGeneric<Account>, IAccountRepository

{
    public AccountRepository(BookingManagementDBContext context) : base(context) { }

    
}
