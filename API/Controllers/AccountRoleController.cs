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

    public AccountRoleController(IAccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _accountRoleRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        var data = result.Select(x => (AccountRoleDto)x);
        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _accountRoleRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok((AccountRoleDto)result);
    }

    [HttpPost]
    public IActionResult Create(CreateAccountRoleDto accountRoleDto)
    {
        var result = _accountRoleRepository.Create(accountRoleDto);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((AccountRoleDto)result);
    }

    [HttpPut]
    public IActionResult Update(AccountRoleDto accountRoleDto)
    {

        var existingEmployee = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);

        if (existingEmployee == null)
        {
            return NotFound("Employee not found");
        }

        AccountRole toUpdate = accountRoleDto;
        toUpdate.CreatedDate = existingEmployee.CreatedDate;

        var result = _accountRoleRepository.Update(toUpdate);
        if (!result)
        {
            return BadRequest("Failed to update data");
        }

        return Ok("Data Updated");
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var existingAccount = _accountRoleRepository.GetByGuid(guid);

        if (existingAccount is null)
        {
            return NotFound("Account not found");
        }

        var deleted = _accountRoleRepository.Delete(existingAccount);

        if (!deleted)
        {
            return BadRequest("Failed to delete account");
        }

        return NoContent(); // Kode status 204 No Content untuk sukses penghapusan tanpa respons.
    }
}




