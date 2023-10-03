using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class BookingRepository : AllRepositoryGeneric<Booking>, IBookingRepository

{
    //injection dbcontect
    public BookingRepository(BookingManagementDBContext context) : base(context) { }

}