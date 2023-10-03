using API.Contracts;
using API.DTOs.Educations;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class EducationController : ControllerBase
{
    //GENERIC
    /* public EducationController(IAllRepository<Education> repositoryT) : base(repositoryT)
     {

     }*/

    //Non Generic
    private readonly IEducationRepository _educationRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IEducationRepository
    public EducationController(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    [HttpGet] //menangani request get all data endpoint /Education
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Education dari _educationRepository.
        var result = _educationRepository.GetAll();
        if (!result.Any()) //cek apakah data ditemukan
        {
            return NotFound("Data Not Found"); //return NotFound jika data tidak ditemukan 
        }

        // mengubah data Education ke dalam format DTO secara explicit.
        var data = result.Select(x => (EducationDto)x);

        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(data);
    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Education/{guid}
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _educationRepository.GetByGuid(guid);
        // cek apakah data result kosong atau apakah data berdasarkan guid ditemukan
        if (result is null)
        {
            return NotFound("Id Not Found"); //return Notfound jika data tidak ditemukan
        }

        //return HTTP OK dengan kode status 200 untuk success
        //return data Education berdasarkan format DTO secara explicit
        return Ok((EducationDto)result);
    }

    [HttpPost] //menangani request create data ke endpoint /Education
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateEducationDto educationDto)
    {
        // create data Education menggunakan format data DTO implisit
        var result = _educationRepository.Create(educationDto);
        if (result is null) //cek apakah create data gagal atau result nya kosong
        {
            // Mengembalikan pesan BadRequest jika gagal create data Education
            return BadRequest("Failed to create data");
        }

        //return HTTP Ok dengan kode status 200 untuk success
        //return data Education yang baru dibuat berdasarkan format DTO secara explicit

        return Ok((EducationDto)result);
    }

    [HttpPut] //menangani request update ke endpoint /Education
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(EducationDto educationDto)
    {
        //get data by guid dan menggunakan format DTO 
        var entity = _educationRepository.GetByGuid(educationDto.Guid);
        if (entity is null) //cek apakah data berdasarkan guid tersedia 
        {
            //return Not Found jika data tidak ditemukan
            return NotFound("Id Not Found");
        }
        //convert data DTO dari inputan user menjadi objek Education
        Education toUpdate = educationDto;
        //menyimpan createdate yg lama 
        toUpdate.CreatedDate = entity.CreatedDate;

        //update Education dalam repository
        var result = _educationRepository.Update(toUpdate);
        if (!result) //cek apakah update data gagal
        {
            // return pesan BadRequest jika gagal update data
            return BadRequest("Failed to update data");
        }
        // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
        return Ok("Data Updated");

    }

    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Education
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah Education dengan guid yang diberikan ada dalam database.
        var entity = _educationRepository.GetByGuid(guid);
        // cek apakah entity (get data by guid) kosong
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }

        //delete Education dari repository
        var result = _educationRepository.Delete(entity);
        if (!result) //cek apakah data gagal di delete
        {
            //return pesan BadRequest jika gagal menghapus Education
            return BadRequest("Failed to delete data");
        }

        // return HTTP OK dan "true" dengan kode status 200 dan untuk sukses delete.
        return Ok("Data Deleted");
    }


}

