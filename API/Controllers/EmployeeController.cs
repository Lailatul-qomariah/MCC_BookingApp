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

    //Non Generic
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _employeeRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _employeeRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Employee employee)
    {
        var result = _employeeRepository.Create(employee);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] Employee employee)
    {
        var existingEmployee = _employeeRepository.GetByGuid(guid);

        if (existingEmployee == null)
        {
            return NotFound("Role not found");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        existingEmployee.BirthDate = employee.BirthDate; //update code dengan code dari inputan

        var updatedRole = _employeeRepository.Update(existingEmployee);

        if (updatedRole == null)
        {
            return BadRequest("Failed to update university");
        }

        return Ok(updatedRole);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingEmployee = _employeeRepository.GetByGuid(guid);

        if (existingEmployee == null)
        {
            return NotFound("Role not found");
        }

        var deletedEmployee = _employeeRepository.Delete(existingEmployee);

        if (deletedEmployee == null)
        {
            return BadRequest("Failed to delete Role");
        }

        return Ok(deletedEmployee);
    }


}

