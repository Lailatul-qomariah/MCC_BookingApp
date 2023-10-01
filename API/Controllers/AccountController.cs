using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class AccountController : GenericAllController<Account>
{

    public AccountController(IAllRepository<Account> repositoryT) : base(repositoryT)
    {
        
    }


   /* private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _accountRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _accountRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Account account)
    {
        var result = _accountRepository.Create(account);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] Account account)
    {
        var existingAccount = _accountRepository.GetByGuid(guid);

        if (existingAccount == null)
        {
            return NotFound("Data not found");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //booking
        existingAccount.Otp = account.Otp; //update code dengan code dari inputan

        var updatedAccount = _accountRepository.Update(existingAccount);

        if (updatedAccount == null)
        {
            return BadRequest("Failed to update data");
        }

        return Ok(updatedAccount);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingAccount = _accountRepository.GetByGuid(guid);

        if (existingAccount == null)
        {
            return NotFound("Data not found");
        }

        var deletedAccount = _accountRepository.Delete(existingAccount);

        if (deletedAccount == null)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok(deletedAccount);
    }*/


}

