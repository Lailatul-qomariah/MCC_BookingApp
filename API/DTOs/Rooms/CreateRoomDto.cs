
using API.Models;

namespace API.DTOs.Rooms
{
    public class CreateRoomDto
    // Representasi DTO untuk membuat entitas Room
    {
        //properti Room yang akan dibuat
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }

        // Operator implisit untuk mengubah objek CreateRoomDto menjadi Room
        public static implicit operator Room(CreateRoomDto createRoomDto)
        {
            // Inisiasi objek Room dengan data dari objek CreateRoomDto
            return new Room
            {
                Name = createRoomDto.Name,
                Floor = createRoomDto.Floor,
                Capacity = createRoomDto.Capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
