using API.Contracts;
using API.DTOs.Universities;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class UniversityController : ControllerBase
{
    //GENERIC
    /*public UniversityController(IAllRepository<University> repositoryT) : base(repositoryT){}
*/

    //Non Generic
    private readonly IUniversityRepository _universityRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IUniversityRepository
    public UniversityController(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    [HttpGet] //menangani request get all data endpoint /University
    public IActionResult GetAll()
    {
        // Mendapatkan semua data universitas dari _universityRepository.
        var result = _universityRepository.GetAll();
        if (!result.Any()) //cek apakah data ditemukan
        {
            return NotFound("Data Not Found"); //return NotFound jika data tidak ditemukan 
        }

        // mengubah data university ke dalam format DTO secara explicit.
        var data = result.Select(x => (UniversityDto)x);

        /*var universityDto = new List<UniversityDto>();
        foreach (var university in result)
        {
            universityDto.Add((UniversityDto) university);
        }*/

        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(data);

    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /university/{guid}
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _universityRepository.GetByGuid(guid);
        // cek apakah data result kosong atau apakah data berdasarkan guid ditemukan
        if (result is null) 
        {
            return NotFound("Id Not Found"); //return Notfound jika data tidak ditemukan
        }

        //return HTTP OK dengan kode status 200 untuk success
        //return data university berdasarkan format DTO secara explicit
        return Ok((UniversityDto)result);
    }


    [HttpPost] //menangani request create data ke endpoint /university
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateUniversityDto universityDto) 
    {
        // create data university menggunakan format data DTO implisit
        var result = _universityRepository.Create(universityDto); 
        if (result is null) //cek apakah create data gagal atau result nya kosong
        {
            // Mengembalikan pesan BadRequest jika gagal create data university
            return BadRequest("Failed to create data");
        }

        //return HTTP Ok dengan kode status 200 untuk success
        //return data university yang baru dibuat berdasarkan format DTO secara explicit
        return Ok((UniversityDto)result);
    }

    [HttpPut] //menangani request update ke endpoint /university
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(UniversityDto universityDto) 
    {
        //get data by guid dan menggunakan format DTO 
        var entity = _universityRepository.GetByGuid(universityDto.Guid);
        if (entity is null) //cek apakah data berdasarkan guid tersedia 
        {
            //return Not Found jika data tidak ditemukan
            return NotFound("Id Not Found");
        }
        //convert data DTO dari inputan user menjadi objek University
        University toUpdate = universityDto;
        //menyimpan createdate yg lama 
        toUpdate.CreatedDate = entity.CreatedDate;
        //update university dalam repository
        var result = _universityRepository.Update(toUpdate);
        if (!result) //cek apakah update data gagal
        {
            // return pesan BadRequest jika gagal update data
            return BadRequest("Failed to update data");
        }
        // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
        return Ok("Data Updated");
    }



    [HttpDelete("{guid}")] //menangani request delete ke endpoint /University
    public IActionResult Delete(Guid guid)
    {
        // get data university by guid
        var existingUniversity = _universityRepository.GetByGuid(guid);
        // cek apakah existingUniversity (get data by guid) kosong
        if (existingUniversity == null)
        {
            // Mengembalikan pesan NotFound jika universitas tidak ditemukan
             return NotFound("University not found");
        }
        //delete university dari repository
        var deletedUniversity = _universityRepository.Delete(existingUniversity);

        if (deletedUniversity == null) //cek apakah data berhasil di delete
        {
        //return pesan BadRequest jika gagal menghapus universitas
        return BadRequest("Failed to delete university");
        }

        // return HTTP OK dan "true" dengan kode status 200 dan untuk sukses delete.
        return Ok("Data Deleted");
    }


}
