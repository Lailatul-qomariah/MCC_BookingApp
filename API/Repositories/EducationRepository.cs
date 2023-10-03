using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EducationRepository : AllRepositoryGeneric<Education>, IEducationRepository

{
    //injection dbcontext
    public EducationRepository(BookingManagementDBContext context) : base(context) { }
}