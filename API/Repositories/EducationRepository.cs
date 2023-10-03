using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EducationRepository : AllRepositoryGeneric<Education>, IEducationRepository
//inheritance pada genericrepository dan interface repository
{
    //injection dbcontext
    public EducationRepository(BookingManagementDBContext context) : base(context) { }
}