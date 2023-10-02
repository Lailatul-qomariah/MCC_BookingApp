using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class UniversityController : ControllerBase
{
    //GENERIC
    /*public UniversityController(IAllRepository<University> repositoryT) : base(repositoryT)
    {

    }
*/



    //Non Generic
    private readonly IUniversityRepository _universityRepository;

    public UniversityController(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _universityRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _universityRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(University university)
    {
        var result = _universityRepository.Create(university);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] University university)
    {
        var existingUniversity = _universityRepository.GetByGuid(guid);

        if (existingUniversity == null)
        {
            return NotFound("University not found");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        existingUniversity.Code = university.Code; //update code dengan code dari inputan
        existingUniversity.Name = university.Name; //update name dengan name baru yang ada di inputan

        var updatedUniversity = _universityRepository.Update(existingUniversity);

        if (updatedUniversity == null)
        {
            return BadRequest("Failed to update university");
        }

        return Ok(updatedUniversity);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingUniversity = _universityRepository.GetByGuid(guid);

        if (existingUniversity == null)
        {
            return NotFound("University not found");
        }

        var deletedUniversity = _universityRepository.Delete(existingUniversity);

        if (deletedUniversity == null)
        {
            return BadRequest("Failed to delete university");
        }

        return Ok(deletedUniversity);
    }


}
