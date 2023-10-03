using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{
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
            //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // mengubah data Account ke dalam format DTO secara explicit.
        var data = result.Select(x => (AccountDto)x);
        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
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
        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
    }

    [HttpPost] //menangani request create data ke endpoint /Account
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateAccountDto accountDto)
    {
        try
        {
            Account toCreate = accountDto;
            toCreate.Password = HashHandler.HashPassword(accountDto.Password);
            // create data university menggunakan format data DTO implisit
            var result = _accountRepository.Create(toCreate);

            //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
            return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
        }
        catch (Exception ex)
        {
            // return dengan kode status 500 dan menampilkan pesan exception 
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to create data",
                Error = ex.Message
            });
        }


    }

    [HttpPut] //menangani request update ke endpoint /Account
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(AccountDto accountDto)
    {
        try
        {
            //get data by guid dan menggunakan format DTO 
            var entity = _accountRepository.GetByGuid(accountDto.Guid);
            if (entity is null) //cek apakah data berdasarkan guid tersedia 
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            //convert data DTO dari inputan user menjadi objek Account
            Account toUpdate = accountDto;
            //menyimpan createdate yg lama 
            toUpdate.CreatedDate = entity.CreatedDate;
            toUpdate.Password = HashHandler.HashPassword(accountDto.Password);

            //update Account dalam repository
            _accountRepository.Update(toUpdate);

            // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
            return Ok(new ResponseOKHandler<string>("Data Updated"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Update data",
                Error = ex.Message
            });
        }
    }

    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Account
    public IActionResult Delete(Guid guid)
    {

        try
        {
            // get data account by guid
            var entity = _accountRepository.GetByGuid(guid);
            // cek apakah entity (get data by guid) kosong
            if (entity is null)
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            //delete Account dari repository
            _accountRepository.Delete(entity);

            // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
            return Ok(new ResponseOKHandler<string>("Data Deleted"));

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Delete data",
                Error = ex.Message
            });
        }



    }
}

