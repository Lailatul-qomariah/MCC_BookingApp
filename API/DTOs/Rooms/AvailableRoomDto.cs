namespace API.DTOs.Rooms
{
    public class AvailableRoomDto
    {
        public Guid Guid { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
    }
}
