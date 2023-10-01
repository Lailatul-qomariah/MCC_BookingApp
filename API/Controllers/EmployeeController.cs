using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class EmployeeController : GenericAllController<Employee>
{

    public EmployeeController(IAllRepository<Employee> repositoryT) : base(repositoryT)
    {
        
    }

   /* [HttpGet]
    public IActionResult GetAll()
    {
        var result = _repositoryT.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _repositoryT.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(T T)
    {
        var result = _repositoryT.Create(T);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] T T)
    {
        var existingRepository = _repositoryT.GetByGuid(guid);

        if (existingRepository == null)
        {
            return NotFound("University not found");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        *//*   existingUniversity.Code = university.Code; //update code dengan code dari inputan
           existingUniversity.Name = university.Name; //update name dengan name baru yang ada di inputan*//*

        var updatedRepository = _repositoryT.Update(existingRepository);

        if (updatedRepository == null)
        {
            return BadRequest("Failed to update university");
        }

        return Ok(updatedRepository);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingRepository = _repositoryT.GetByGuid(guid);

        if (existingRepository == null)
        {
            return NotFound($"{_repositoryT} not found");
        }

        var deletedRepository = _repositoryT.Delete(existingRepository);

        if (deletedRepository == null)
        {
            return BadRequest($"Failed to delete {_repositoryT}");
        }

        return Ok(deletedRepository);
    }*/


}

