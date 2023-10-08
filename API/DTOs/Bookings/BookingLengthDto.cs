namespace API.DTOs.Bookings
{
    public class BookingLengthDto
    //representasi DTO untuk data yang akan ditampilkan pada user
    {
        public Guid RoomGuid { get; set; }
        public string RoomName { get; set; }
        public string BookingLength { get; set; }
    }
}
