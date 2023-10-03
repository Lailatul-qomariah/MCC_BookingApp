using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class AccountRoleController : ControllerBase
{
    //inheritance ke genericAllController
    /*public AccountRoleController(IGenericRepository<AccountRole> repositoryT) : base(repositoryT)
    {
        
    }
*/

    private readonly IAccountRoleRepository _accountRoleRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IAccountRoleRepository
    public AccountRoleController(IAccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }

    [HttpGet] //menangani request get all data endpoint /AccountRole
    public IActionResult GetAll()
    {
        // Mendapatkan semua data universitas dari _accountRoleRepository.
        var result = _accountRoleRepository.GetAll();
        if (!result.Any()) //cek apakah data ditemukan
        {
            return NotFound("Data Not Found"); //return NotFound jika data tidak ditemukan 
        }

        // mengubah data AccountRole ke dalam format DTO secara explicit.
        var data = result.Select(x => (AccountRoleDto)x);
        return Ok(result);
    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /AccountRole/{guid}
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _accountRoleRepository.GetByGuid(guid);
        // cek apakah data result kosong atau apakah data berdasarkan guid ditemukan
        if (result is null)
        {
            return NotFound("Id Not Found");
        }

        //return HTTP OK dengan kode status 200 untuk success
        //return data AccountRole berdasarkan format DTO secara explicit
        return Ok((AccountRoleDto)result);
    }

    [HttpPost] //menangani request create data ke endpoint /AccountRole
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateAccountRoleDto accountRoleDto)
    {
        // create data AccountRole menggunakan format data DTO implisit
        var result = _accountRoleRepository.Create(accountRoleDto);
        if (result is null) //cek apakah create data gagal atau result nya kosong
        {
            // Mengembalikan pesan BadRequest jika gagal create data AccountRole
            return BadRequest("Failed to create data");
        }

        //return HTTP Ok dengan kode status 200 untuk success
        //return data AccountRole yang baru dibuat berdasarkan format DTO secara explicit
        return Ok((AccountRoleDto)result);
    }

    [HttpPut] //menangani request update ke endpoint /AccountRole
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(AccountRoleDto accountRoleDto)
    {

        //get data by guid dan menggunakan format DTO 
        var entity = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);

        if (entity is null) //cek apakah data berdasarkan guid tersedia 
        {
            //return Not Found jika data tidak ditemukan
            return NotFound("Id Not Found");
        }
        //convert data DTO dari inputan user menjadi objek AccountRole
        AccountRole toUpdate = accountRoleDto;
        //menyimpan createdate yg lama 
        toUpdate.CreatedDate = entity.CreatedDate;

        //update AccountRole dalam repository
        var result = _accountRoleRepository.Update(toUpdate);
        if (!result) //cek apakah update data gagal
        {
            // return pesan BadRequest jika gagal update data
            return BadRequest("Failed to update data");
        }
        // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
        return Ok("Data Updated");
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // get data account role by guid
        var existingAccount = _accountRoleRepository.GetByGuid(guid);

        // Periksa apakah AccountRole dengan guid yang diberikan ada dalam database.
        if (existingAccount is null)
        {
            // Mengembalikan pesan NotFound jika universitas tidak ditemukan
            return NotFound("Account not found");
        }

        //delete AccountRole dari repository
        var deleted = _accountRoleRepository.Delete(existingAccount);

        if (!deleted)
        {
            //return pesan BadRequest jika gagal menghapus universitas
            return BadRequest("Failed to delete account");
        }
        // return HTTP OK dan "true" dengan kode status 200 dan untuk sukses delete.
        return Ok("Data Deleted");
    }
}




