using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class EducationController : ControllerBase
{
    //GENERIC
    /* public EducationController(IAllRepository<Education> repositoryT) : base(repositoryT)
     {

     }*/

    //Non Generic
    private readonly IEducationRepository _educationRepository;

    public EducationController(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _educationRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _educationRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Education education)
    {
        var result = _educationRepository.Create(education);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] Education education)
    {
        var existingEducation = _educationRepository.GetByGuid(guid);

        if (existingEducation == null)
        {
            return NotFound("Data not found");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //education
        existingEducation.Major = education.Major; //update code dengan code dari inputan
        existingEducation.Degree = education.Degree;
        existingEducation.Gpa = education.Gpa;
        existingEducation.UniversityGuid = education.UniversityGuid;

        var updatedEducation = _educationRepository.Update(existingEducation);

        if (updatedEducation == null)
        {
            return BadRequest("Failed to update data");
        }

        return Ok(updatedEducation);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingEducation = _educationRepository.GetByGuid(guid);

        if (existingEducation == null)
        {
            return NotFound("Data not found");
        }

        var deletedEducation = _educationRepository.Delete(existingEducation);

        if (deletedEducation == null)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok(deletedEducation);
    }


}

