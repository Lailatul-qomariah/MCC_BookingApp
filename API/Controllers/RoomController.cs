using API.Contracts;
using API.DTOs.Rooms;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class RoomController : ControllerBase
{
    //GENERIC
    //inheritance ke genericAllController
    /*public RoomController(IGenericRepository<Room> repositoryT) : base(repositoryT)
    {

    }*/



    //Non Generic
    private readonly IRoomRepository _roomRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IRoomRepository
    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    [HttpGet] //menangani request get all data endpoint /Room
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Room dari _roomRepository.
        var result = _roomRepository.GetAll();
        if (!result.Any())//cek apakah data ditemukan
        {
            return NotFound("Data Not Found"); //return NotFound jika data tidak ditemukan 
        }

        // mengubah data Room ke dalam format DTO secara explicit.
        var data = result.Select(x => (RoomDto)x);

       

        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(data);

    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Room/{guid}
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _roomRepository.GetByGuid(guid);
        // cek apakah data result kosong atau apakah data berdasarkan guid ditemukan
        if (result is null)
        {
            return NotFound("Id Not Found"); //return Notfound jika data tidak ditemukan
        }
        
        //return HTTP OK dengan kode status 200 untuk success
        //return data Room berdasarkan format DTO secara explicit
        return Ok((RoomDto)result);
    }


    [HttpPost] //menangani request create data ke endpoint /Room
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateRoomDto roomDto)
    {
        // create data Room menggunakan format data DTO implisit
        var result = _roomRepository.Create(roomDto);
        if (result is null) //cek apakah create data gagal atau result nya kosong
        {
            // Mengembalikan pesan BadRequest jika gagal create data Room
            return BadRequest("Failed to create data");
        }
        //return HTTP Ok dengan kode status 200 untuk success
        //return data Room yang baru dibuat berdasarkan format DTO secara explicit
        return Ok((RoomDto)result);
    }

    [HttpPut] //menangani request update ke endpoint /Room
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(RoomDto roomDto)
    {
        //get data by guid dan menggunakan format DTO 
        var entity = _roomRepository.GetByGuid(roomDto.Guid);
        if (entity is null)//cek apakah data berdasarkan guid tersedia 
        {
            //return Not Found jika data tidak ditemukan
            return NotFound("Id Not Found");
        }

        //convert data DTO dari inputan user menjadi objek Room
        Room toUpdate = roomDto;
        //menyimpan createdate yg lama 
        toUpdate.CreatedDate = entity.CreatedDate;
        //update Room dalam repository
        var result = _roomRepository.Update(toUpdate);
        if (!result) //cek apakah update data gagal
        {
            // return pesan BadRequest jika gagal update data
            return BadRequest("Failed to update data");
        }

        // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
        return Ok("Data Updated");
    }



    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Room
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah Room dengan guid yang diberikan ada dalam database.
        var existingRoom = _roomRepository.GetByGuid(guid);

        // cek apakah existingRoom (get data by guid) kosong
        if (existingRoom == null)
        {
            // Mengembalikan pesan NotFound jika Room tidak ditemukan
            return NotFound("Data not found");
        }

        //delete Room dari repository
        var deletedRoom = _roomRepository.Delete(existingRoom);

        if (deletedRoom == null) //cek apakah data berhasil di delete
        {
            //return pesan BadRequest jika gagal menghapus Room
            return BadRequest("Failed to delete data");
        }

        // return HTTP OK dan "true" dengan kode status 200 dan untuk sukses delete.
        return Ok(deletedRoom);
    }
}

