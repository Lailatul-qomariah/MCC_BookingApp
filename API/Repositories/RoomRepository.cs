using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class RoomRepository : AllRepositoryGeneric<Room>, IRoomRepository
//inheritance pada genericrepository dan interface repository
{
    //injection dbcontext
    public RoomRepository(BookingManagementDBContext context) : base(context) { }


}