using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class EmployeeController : ControllerBase
{
    //GENERIC
    /*public EmployeeController(IGenericRepository<Employee> repositoryT) : base(repositoryT)
    {

    }*/

    //Non Generic
    private readonly IEmployeeRepository _employeeRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IEmployeeRepository
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet] //menangani request get all data endpoint /Employee
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Employee dari _employeeRepository.
        var result = _employeeRepository.GetAll();
        if (!result.Any()) //cek apakah data ditemukan
        {
            return NotFound("Data Not Found"); //return NotFound jika data tidak ditemukan 
        }
        // mengubah data Employee ke dalam format DTO secara explicit.
        var data = result.Select(x => (EmployeeDto)x);
        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(result);
    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Employee/{guid}

    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _employeeRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found"); //return Notfound jika data tidak ditemukan
        }

        //return HTTP OK dengan kode status 200 untuk success
        //return data Employee berdasarkan format DTO secara explicit
        return Ok((EmployeeDto)result);
    }

    [HttpPost] //menangani request create data ke endpoint /Employee
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateEmployeeDto createEmpDto)
    {
        // create data Employee menggunakan format data DTO implisit
        var result = _employeeRepository.Create(createEmpDto);
        if (result is null) //cek apakah create data gagal atau result nya kosong
        {
            // Mengembalikan pesan BadRequest jika gagal create data Employee
            return BadRequest("Failed to create data");
        }

        //return HTTP Ok dengan kode status 200 untuk success
        //return data Employee yang baru dibuat berdasarkan format DTO secara explicit

        return Ok((EmployeeDto)result);
    }

    [HttpPut] //menangani request update ke endpoint /Employee
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(EmployeeDto employeeDto)
    {

        //get data by guid dan menggunakan format DTO 
        var existingEmployee = _employeeRepository.GetByGuid(employeeDto.Guid);

        if (existingEmployee is null) //cek apakah data berdasarkan guid tersedia 
        {
            //return Not Found jika data tidak ditemukan
            return NotFound("Id Not Found");
        }
        //convert data DTO dari inputan user menjadi objek Employee
        Employee toUpdate = employeeDto;
        //menyimpan createdate yg lama 
        toUpdate.CreatedDate = existingEmployee.CreatedDate;

        var result = _employeeRepository.Update(toUpdate);
        if (!result) //cek apakah update data gagal
        {
            // return pesan BadRequest jika gagal update data
            return BadRequest("Failed to update data");
        }
        // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
        return Ok("Data Updated");
    }


    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Employee
    public IActionResult Delete(Guid guid)
    {
        // get data account role by guid
        var existingEmployee = _employeeRepository.GetByGuid(guid);

        // cek apakah existingEmployee (get data by guid) kosong
        if (existingEmployee is null)
        {
            // Mengembalikan pesan NotFound jika Employee tidak ditemukan
            return NotFound("Employee not found");
        }

        //delete Employee dari repository
        var deleted = _employeeRepository.Delete(existingEmployee);

        if (!deleted) //cek apakah data gagal di delete
        {
            //return pesan BadRequest jika gagal menghapus Employee
            return BadRequest("Failed to delete employee");
        }

        // return HTTP OK dan "true" dengan kode status 200 dan untuk sukses delete.
        return Ok("Data Updated");
    }


}

