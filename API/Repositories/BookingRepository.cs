using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class BookingRepository : AllRepositoryGeneric<Booking>, IBookingRepository
//inheritance pada genericrepository dan interface repository
{
    //injection dbcontect
    public BookingRepository(BookingManagementDBContext context) : base(context) { }


}