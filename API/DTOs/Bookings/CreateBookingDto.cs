
using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Bookings
{
    public class CreateBookingDto
    // Representasi DTO untuk membuat entitas Booking
    {
        //properti Booking yang akan dibuat
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevels Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        // Operator implisit untuk mengubah objek CreateBookingDto menjadi objek Booking
        public static implicit operator Booking(CreateBookingDto bookingDto)
        {
            // Inisiasi objek Booking dengan data dari objek CreateBookingDto
            return new Booking
            {
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                Status = bookingDto.Status,
                Remarks = bookingDto.Remarks,
                RoomGuid = bookingDto.RoomGuid,
                EmployeeGuid = bookingDto.EmployeeGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        
    }
}
