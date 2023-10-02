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

        var data = result.Select(x => (AccountDto)x);

        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _accountRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok((AccountDto)result);
    }

    [HttpPost]
    public IActionResult Create(CreateAccountDto accountDto)
    {
        var result = _accountRepository.Create(accountDto);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((AccountDto)result);
    }

    [HttpPut]
    public IActionResult Update(AccountDto accountDto)
    {
        var entity = _accountRepository.GetByGuid(accountDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }

        Account toUpdate = accountDto;
        toUpdate.CreatedDate = entity.CreatedDate;

        var result = _accountRepository.Update(toUpdate);
        if (!result)
        {
            return BadRequest("Failed to update data");
        }

        return Ok("Data Updated");

    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var entity = _accountRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }

        var result = _accountRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok("Data Deleted");
    }


}

