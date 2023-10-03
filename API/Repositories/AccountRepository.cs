using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRepository : AllRepositoryGeneric<Account>, IAccountRepository
//inheritance pada genericrepository dan interface repository
{
    //injection dbcontext
    public AccountRepository(BookingManagementDBContext context) : base(context) { }

    
}
