using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Bookings
{
    public class BookingDto
    {
        //representasi DTO untuk model atau entitaas Booking
        public Guid Guid {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevels Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        // Operator eksplisit untuk convert objek Booking ke BookingDto
        //digunakan atau dipanggil pada method GetAll, GetByGuid dan Create
        public static explicit operator BookingDto(Booking booking)
        {
            // Inisiasi objek BookingDto dengan data dari objek Booking
            return new BookingDto
            {
                Guid = booking.Guid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid,

            };
        }

        // Operator implisit untuk convert objek BookingDto ke Booking
        //digunakan pada saat menggunakan method Update
        public static implicit operator Booking(BookingDto bookingDto)
        {
            // Inisiasi objek Booking dengan data dari objek BookingDto
            return new Booking
            {
                Guid = bookingDto.Guid,
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                Status = bookingDto.Status,
                Remarks = bookingDto.Remarks,
                RoomGuid = bookingDto.RoomGuid,
                EmployeeGuid = bookingDto.EmployeeGuid,
                ModifiedDate = DateTime.Now
            };
        }


    }
}
