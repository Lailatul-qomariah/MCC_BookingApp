using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IEmployeeRepository
    public EmployeeController(IEmployeeRepository employeeRepository, IEducationRepository educationRepository,
        IUniversityRepository universityRepository)
    {
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }

    [HttpGet] //menangani request get all data endpoint /Employee
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Employee dari _employeeRepository.
        var result = _employeeRepository.GetAll();
        if (!result.Any()) //cek apakah data ditemukan
        {
            //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // mengubah data Employee ke dalam format DTO secara explicit.
        var data = result.Select(x => (EmployeeDto)x);

        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<IEnumerable<EmployeeDto>>(data));
    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Employee/{guid}
    
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _employeeRepository.GetByGuid(guid);
        if (result is null)
        {
            //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)result));
    }

    [HttpGet("details")]
   
    public IActionResult GetDetails() 
    {
        //get all data dari entity employee, education dan university
        var employees = _employeeRepository.GetAll();
        var education = _educationRepository.GetAll();
        var universities = _universityRepository.GetAll();
        
        //cek apakah datanya ada
        if (!(employees.Any() && education.Any() && universities.Any()))
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });

        }
        //join tabel agar datanya bisa diset dan direturn dalam format dto
        var employeeDetails = from emp in employees
                              join edu in education on emp.Guid equals edu.Guid
                              join univ in universities on edu.UniversityGuid equals univ.Guid
                              select new EmployeeDetailsDto
                              {
                                  Guid = emp.Guid,
                                  Nik = emp.Nik,
                                  FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                                  BirthDate = emp.BirthDate,
                                  Gender = emp.Gender.ToString(),
                                  HiringDate = emp.HiringDate,
                                  Email = emp.Email,
                                  PhoneNumber = emp.PhoneNumber,
                                  Major = edu.Major,
                                  Degree = edu.Degree,
                                  Gpa = edu.Gpa,
                                  University = univ.Name
                              };
                              
        return Ok(new ResponseOKHandler<IEnumerable<EmployeeDetailsDto>>(employeeDetails));
    }

    [HttpPost]
  
    public IActionResult Create(CreateEmployeeDto createEmpDto)
    {
        try
        {
            Employee toCreate = createEmpDto;
            //set Nik menggunakan generate nik
            toCreate.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastNik());
            var result = _employeeRepository.Create(toCreate);

            //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
            return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)result));
        }
        catch (Exception ex)
        {
            // return dengan kode status 500 dan menampilkan pesan exception 
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to create data",
                Error = ex.Message
            });
           // return StatusCode(StatusCodes.Status500InternalServerError, new ResponeErrorTry<"xcxcxc">(ex.Message));

        }

    }

    [HttpPut] //menangani request update ke endpoint /Employee
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
   
    public IActionResult Update(EmployeeDto employeeDto)
    {
        try
        {

            //get data by guid dan menggunakan format DTO 
            var existingEmployee = _employeeRepository.GetByGuid(employeeDto.Guid);

            if (existingEmployee is null) //cek apakah data berdasarkan guid tersedia 
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            //convert data DTO dari inputan user menjadi objek Employee
            Employee toUpdate = employeeDto;
            //menyimpan data nik & CreatedDate yang lama
            toUpdate.Nik = existingEmployee.Nik;
            toUpdate.CreatedDate = existingEmployee.CreatedDate;

           _employeeRepository.Update(toUpdate);

            // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
            return Ok(new ResponseOKHandler<string>("Data Updated"));
        }
        catch (Exception ex)
        {
            // return dengan kode status 500 dan menampilkan pesan exception 
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Update data",
                Error = ex.Message
            });
        }
    }

    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Employee
    public IActionResult Delete(Guid guid)
    {

        try
        {
            // get data employee by guid
            var existingEmployee = _employeeRepository.GetByGuid(guid);

            // cek apakah existingEmployee (get data by guid) kosong
            if (existingEmployee is null)
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            //delete Employee dari repository
            _employeeRepository.Delete(existingEmployee);

            // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
            return Ok(new ResponseOKHandler<string>("Data Deleted"));
        }
        catch (Exception ex)
        {
            // return dengan kode status 500 dan menampilkan pesan exception 
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Delete data",
                Error = ex.Message
            });
        }


    }


    
}

