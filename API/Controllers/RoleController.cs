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

        var data = result.Select(x => (RoleDto)x);

        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roleRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok((RoleDto)result);
    }

    [HttpPost]
    public IActionResult Create(CreateRoleDto roleDto)
    {
        var result = _roleRepository.Create(roleDto);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((RoleDto)result);
    }

    [HttpPut]
    public IActionResult Update(RoleDto roleDto)
    {
        var entity = _roleRepository.GetByGuid(roleDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }

        Role toUpdate = roleDto;
        toUpdate.CreatedDate = entity.CreatedDate;

        var result = _roleRepository.Update(toUpdate);
        if (!result)
        {
            return BadRequest("Failed to update data");
        }

        return Ok("Data Updated");

    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var entity = _roleRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }

        var result = _roleRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok("Data Deleted");
    }


}

