
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Client.Controllers;
[Authorize(Roles ="admin")]
public class RoleController : Controller // Inheritance ke class controller bawaan 
{
    private readonly IRoleRepository _repository; // Deklarasi variabel repository yang merupakan instance dari IEmployeeRepository.

    public RoleController(IRoleRepository repository) // Konstruktor EmployeeController dengan parameter repository.
    {
        _repository = repository; // Dependency injection
    }

    public async Task<IActionResult> Index() // Metode List untuk get data employee.
    {
       return View();
    }
    public async Task<JsonResult> GetAllRole()
    {
        var result = await _repository.Get();
        return Json(result);
    }
}

