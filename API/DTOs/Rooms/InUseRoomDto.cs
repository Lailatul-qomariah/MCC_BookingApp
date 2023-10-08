
namespace API.DTOs.Rooms
{
    public class InUseRoomDto
    //representasi DTO untuk data yang akan ditampilkan pada user
    {
        public Guid BookingGuid { get; set; }
        public string RoomName { get; set; }
        public string Status { get; set; }
        public int Floor { get; set; }
        public string BookedBy { get; set; }
    }
}
