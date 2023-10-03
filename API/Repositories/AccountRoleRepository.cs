using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRoleRepository : AllRepositoryGeneric<AccountRole>, IAccountRoleRepository
//inharitance pada genericrepository dan interface repository

{
    //injection dbcontect
    public AccountRoleRepository(BookingManagementDBContext context) : base(context) { }

  
}