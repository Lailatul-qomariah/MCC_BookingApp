using API.Contracts;
using API.Data;
using API.Models;


namespace API.Repositories;

public class AccountRoleRepository : AllRepositoryGeneric<AccountRole>, IAccountRoleRepository
//inharitance pada genericrepository dan interface repository

{
    //injection dbcontect
    public AccountRoleRepository(BookingManagementDBContext context) : base(context) { }

  
}