using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : AllRepositoryGeneric<University>, IUniversityRepository
//inheritance pada genericrepository dan interface repository
{
    //injection dbcontext
    public UniversityRepository(BookingManagementDBContext context) : base(context) { }




}