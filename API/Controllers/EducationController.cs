using API.Contracts;
using API.DTOs.Educations;
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

        var data = result.Select(x => (EducationDto)x);

        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _educationRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok((EducationDto)result);
    }

    [HttpPost]
    public IActionResult Create(CreateEducationDto educationDto)
    {
        var result = _educationRepository.Create(educationDto);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((EducationDto)result);
    }

    [HttpPut]
    public IActionResult Update(EducationDto educationDto)
    {
        var entity = _educationRepository.GetByGuid(educationDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }

        Education toUpdate = educationDto;
        toUpdate.CreatedDate = entity.CreatedDate;

        var result = _educationRepository.Update(toUpdate);
        if (!result)
        {
            return BadRequest("Failed to update data");
        }

        return Ok("Data Updated");

    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var entity = _educationRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }

        var result = _educationRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok("Data Deleted");
    }


}

