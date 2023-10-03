using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRoleRepository : AllRepositoryGeneric<AccountRole>, IAccountRoleRepository

{
    public AccountRoleRepository(BookingManagementDBContext context) : base(context) { }

  
}