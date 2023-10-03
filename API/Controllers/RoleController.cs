using API.Contracts;
using API.DTOs.Roles;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class RoleController : ControllerBase
{
    //GENERIC
    //inheritance ke genericAllController
    /* public RoleController(IGenericRepository<Role> repositoryT) : base(repositoryT)
     {

     }*/


    //Non Generic
    private readonly IRoleRepository _roleRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IRoleRepository
    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet] //menangani request get all data endpoint /Role
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Role dari _roleRepository.
        var result = _roleRepository.GetAll();
        if (!result.Any()) //cek apakah data ditemukan
        {
            return NotFound("Data Not Found"); //return NotFound jika data tidak ditemukan 
        }

        // mengubah data Role ke dalam format DTO secara explicit.
        var data = result.Select(x => (RoleDto)x);

        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(data);
    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Role/{guid}
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _roleRepository.GetByGuid(guid);
        // cek apakah data result kosong atau apakah data berdasarkan guid ditemukan
        if (result is null)
        {
            return NotFound("Id Not Found"); //return Notfound jika data tidak ditemukan
        }
        //return HTTP OK dengan kode status 200 untuk success
        //return data Role berdasarkan format DTO secara explicit
        return Ok((RoleDto)result);
    }

    [HttpPost] //menangani request create data ke endpoint /Role
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateRoleDto roleDto)
    {
        // create data Role menggunakan format data DTO implisit
        var result = _roleRepository.Create(roleDto);
        if (result is null)
        {
            // Mengembalikan pesan BadRequest jika gagal create data Role
            return BadRequest("Failed to create data");
        }

        //return HTTP Ok dengan kode status 200 untuk success
        //return data Role yang baru dibuat berdasarkan format DTO secara explicit
        return Ok((RoleDto)result);
    }

    [HttpPut]//menangani request update ke endpoint /Role
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(RoleDto roleDto)
    {
        //get data by guid dan menggunakan format DTO 
        var entity = _roleRepository.GetByGuid(roleDto.Guid);
        if (entity is null) //cek apakah data berdasarkan guid tersedia 
        {
            //return Not Found jika data tidak ditemukan
            return NotFound("Id Not Found");
        }

        //convert data DTO dari inputan user menjadi objek Role
        Role toUpdate = roleDto;
        //menyimpan createdate yg lama 
        toUpdate.CreatedDate = entity.CreatedDate;

        //update Role dalam repository
        var result = _roleRepository.Update(toUpdate);
        if (!result) //cek apakah update data gagal
        {
            // return pesan BadRequest jika gagal update data
            return BadRequest("Failed to update data");
        }

        // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
        return Ok("Data Updated");

    }

    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Role
    public IActionResult Delete(Guid guid)
    {
        // get data role by guid
        var entity = _roleRepository.GetByGuid(guid);
        // cek apakah entity (get data by guid) kosong
        if (entity is null)
        {
            // Mengembalikan pesan NotFound jika Role tidak ditemukan
            return NotFound("Id Not Found");
        }

        //delete Role dari repository
        var result = _roleRepository.Delete(entity); 
        if (!result) //cek apakah data gagal di delete
        {
            //return pesan BadRequest jika gagal menghapus Role
            return BadRequest("Failed to delete data");
        }

        // return HTTP OK dan "true" dengan kode status 200 dan untuk sukses delete.
        return Ok("Data Deleted");
    }


}

