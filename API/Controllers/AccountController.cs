using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{
    //inheritance ke genericAllController
    /*public AccountController(IGenericRepository<Account> repositoryT) : base(repositoryT)
    {
        
    }*/


    private readonly IAccountRepository _accountRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IAccountRepository
    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet] //menangani request get all data endpoint /Account
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Account dari _accountRepository.
        var result = _accountRepository.GetAll();
        if (!result.Any()) //cek apakah data ditemukan
        {
            //return jika data tidak ditemukan 
            return NotFound("Data Not Found"); 
        }
        // mengubah data Account ke dalam format DTO secara explicit.
        var data = result.Select(x => (AccountDto)x);
        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(data);
    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Account/{guid}
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _accountRepository.GetByGuid(guid);
        // cek apakah data result kosong atau apakah data berdasarkan guid ditemukan
        if (result is null)
        {
            return NotFound("Id Not Found"); //return Notfound jika data tidak ditemukan
        }     
        //return HTTP OK dengan kode status 200 untuk success
        //return data objek Account berdasarkan format DTO secara explicit
        return Ok((AccountDto)result);
    }

    [HttpPost] //menangani request create data ke endpoint /Account
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateAccountDto accountDto) 
    {
        // create data university menggunakan format data DTO implisit
        var result = _accountRepository.Create(accountDto);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((AccountDto)result);
    }

    [HttpPut] //menangani request update ke endpoint /Account
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(AccountDto accountDto)
    {
        //get data by guid dan menggunakan format DTO 
        var entity = _accountRepository.GetByGuid(accountDto.Guid);
        if (entity is null) //cek apakah data berdasarkan guid tersedia 
        {
            //return Not Found jika data tidak ditemukan
            return NotFound("Id Not Found");
        }
        //convert data DTO dari inputan user menjadi objek Account
        Account toUpdate = accountDto;
        //menyimpan createdate yg lama 
        toUpdate.CreatedDate = entity.CreatedDate;

        //update Account dalam repository
        var result = _accountRepository.Update(toUpdate);
        if (!result) //cek apakah update data gagal

        {
            // return pesan BadRequest jika gagal update data
            return BadRequest("Failed to update data");
        }
        // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
        return Ok("Data Updated");

    }

    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Account
    public IActionResult Delete(Guid guid)
    {
        // get data account by guid
        var entity = _accountRepository.GetByGuid(guid);
        // cek apakah entity (get data by guid) kosong
        if (entity is null)
        {
            // Mengembalikan pesan NotFound jika universitas tidak ditemukan
            return NotFound("Id Not Found");
        }
        //delete Account dari repository
        var result = _accountRepository.Delete(entity);
        if (!result) //cek apakah data berhasil di delete
        {
            //return pesan BadRequest jika gagal delete Account
            return BadRequest("Failed to delete data");
        }
        // return HTTP OK dan "true" dengan kode status 200 dan untuk sukses delete.
        return Ok("Data Deleted");
    }


}

