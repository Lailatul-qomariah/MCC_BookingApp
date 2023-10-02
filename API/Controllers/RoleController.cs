using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class RoleController : GenericAllController<Role>
{
    //GENERIC
    //inheritance ke genericAllController
    public RoleController(IAllRepository<Role> repositoryT) : base(repositoryT)
    {
        
    }



  /*  //Non Generic
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roleRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roleRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Role role)
    {
        var result = _roleRepository.Create(role);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] Role role)
    {
        var existingRole = _roleRepository.GetByGuid(guid);

        if (existingRole == null)
        {
            return NotFound("Data not found");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        existingRole.Name = university.Name; //update name dengan name baru yang ada di inputan

        var updatedRole = _roleRepository.Update(existingRole);

        if (updatedRole == null)
        {
            return BadRequest("Failed to update data");
        }

        return Ok(updatedRole);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingRole = _roleRepository.GetByGuid(guid);

        if (existingRole == null)
        {
            return NotFound("Data not found");
        }

        var deletedRole = _roleRepository.Delete(existingRole);

        if (deletedRole == null)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok(deletedRole);
    }*/


}

