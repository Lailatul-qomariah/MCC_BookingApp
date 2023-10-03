using API.Contracts;
using API.DTOs.Bookings;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class BookingController : ControllerBase
{
    //inheritance ke genericAllController
    /* public BookingController(IGenericRepository<Booking> repositoryT) : base(repositoryT)
     {

     }*/

    //Non Generic
    private readonly IBookingRepository _bookingRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IBookingRepository
    public BookingController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    [HttpGet] //menangani request get all data endpoint /Booking
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Booking dari _bookingRepository.
        var result = _bookingRepository.GetAll();
        if (!result.Any()) //cek apakah data ditemukan
        {
            return NotFound("Data Not Found"); //return NotFound jika data tidak ditemukan 
        }

        // mengubah data Booking ke dalam format DTO secara explicit.
        var data = result.Select(x => (BookingDto)x);

        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(data);

    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Booking/{guid}
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _bookingRepository.GetByGuid(guid);
        // cek apakah data result kosong atau apakah data berdasarkan guid ditemukan
        if (result is null)
        {
            return NotFound("Id Not Found"); //return Notfound jika data tidak ditemukan
        }

        //return HTTP OK dengan kode status 200 untuk success
        //return data Booking berdasarkan format DTO secara explicit
        return Ok((BookingDto)result);
    }

   
    [HttpPost] //menangani request create data ke endpoint /Booking
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateBookingDto bookingDto)
    {
        // create data Booking menggunakan format data DTO implisit
        var result = _bookingRepository.Create(bookingDto);
        if (result is null) //cek apakah create data gagal atau result nya kosong
        {
            // Mengembalikan pesan BadRequest jika gagal create data Booking
            return BadRequest("Failed to create data");
        }

        //return HTTP Ok dengan kode status 200 untuk success
        //return data Booking yang baru dibuat berdasarkan format DTO secara explicit
        return Ok((BookingDto)result);
    }

    [HttpPut] //menangani request update ke endpoint /Booking
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(BookingDto bookingDto)
    {
        //get data by guid dan menggunakan format DTO 
        var entity = _bookingRepository.GetByGuid(bookingDto.Guid);
        if (entity is null) //cek apakah data berdasarkan guid tersedia 
        {
            //return Not Found jika data tidak ditemukan
            return NotFound("Id Not Found");
        }
        //convert data DTO dari inputan user menjadi objek Booking
        Booking toUpdate = bookingDto;
        //menyimpan createdate yg lama 
        toUpdate.CreatedDate = entity.CreatedDate;

        //update Booking dalam repository
        var result = _bookingRepository.Update(toUpdate);
        if (!result) //cek apakah update data gagal
        {
            // return pesan BadRequest jika gagal update data
            return BadRequest("Failed to update data");
        }
        // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
        return Ok("Data Updated");

    }



    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Booking

    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah Booking dengan guid yang diberikan ada dalam database.
        var entity = _bookingRepository.GetByGuid(guid);

        // cek apakah entity (get data by guid) kosong
        if (entity == null)
        {
            // Mengembalikan pesan NotFound jika Booking tidak ditemukan
            return NotFound("Data not found");
        }

        //delete Booking dari repository
        var deleted = _bookingRepository.Delete(entity);

        if (deleted == null) //cek apakah data gagal di delete
        {
            //return pesan BadRequest jika gagal menghapus Booking
            return BadRequest("Failed to delete data");
        }

        // return HTTP OK dan "true" dengan kode status 200 dan untuk sukses delete.
        return Ok("Data Deleted");
    }

}

