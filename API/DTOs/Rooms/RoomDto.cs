using API.DTOs.Universities;
using API.Models;

namespace API.DTOs.Rooms
{
    public class RoomDto
    {
        //representasi DTO untuk model atau entitaas Room
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        // Operator eksplisit untuk convert objek Room ke RoomDto
        //digunakan atau dipanggil pada method GetAll, GetByGuid dan Create dan GetVailableRoom
        public static explicit operator RoomDto(Room room)
        {
            // Inisiasi objek RoomDto dengan data dari objek Room
            return new RoomDto
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity,
            };
        }

        // Operator implisit untuk convert objek RoomDto ke Room
        //digunakan pada saat menggunakan method Update
        public static implicit operator Room(RoomDto roomDto)
        {
            // Inisiasi objek Room dengan data dari objek RoomDto
            return new Room
            {
                Guid = roomDto.Guid,
                Name = roomDto.Name,
                Floor = roomDto.Floor,
                Capacity = roomDto.Capacity,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
