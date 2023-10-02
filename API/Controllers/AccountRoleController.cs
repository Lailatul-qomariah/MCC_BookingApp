using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class AccountRoleController : GenericAllController<AccountRole>
{
    //inheritance ke genericAllController
    public AccountRoleController(IAllRepository<AccountRole> repositoryT) : base(repositoryT)
    {
        
    }


   /* private readonly IAccountRoleRepository _accountRoleRepository;

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
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(AccountRole accountRole)
    {
        var result = _accountRoleRepository.Create(accountRole);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] AccountRole accountRole)
    {
        var existingARole = _accountRoleRepository.GetByGuid(guid);

        if (existingARole == null)
        {
            return NotFound("Data not found");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        existingARole.AccountGuid = accountRole.AccountGuid;
        existingARole.RoleGuid = accountRole.RoleGuid;

        var updatedARole = _accountRoleRepository.Update(existingARole);

        if (updatedARole == null)
        {
            return BadRequest("Failed to update data");
        }

        return Ok(updatedARole);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingARole = _accountRoleRepository.GetByGuid(guid);

        if (existingARole == null)
        {
            return NotFound("Data not found");
        }

        var deletedARole = _accountRoleRepository.Delete(existingARole);

        if (deletedARole == null)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok(deletedARole);
    }*/


}

