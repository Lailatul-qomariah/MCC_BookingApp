using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace Client.Controllers;
public class AuthController : Controller // Inheritance ke class controller bawaan 
{
    private readonly IAccountRepository _repository; // Deklarasi variabel repository yang merupakan instance dari IEmployeeRepository.

    public AuthController(IAccountRepository repository) // Konstruktor EmployeeController dengan parameter repository.
    {
        _repository = repository; // Dependency injection
    }

    public async Task<IActionResult> Login() // Metode List untuk get data employee.
    {
       return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var result = await _repository.Login(login);

        if (result.Status == "OK")
        {

            HttpContext.Session.SetString("JWToken", result.Data.Token);
            return RedirectToAction("List", "Employee");
        }
        return RedirectToAction("Login", "Auth");
    }

    [HttpGet("/Logout/")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Auth");
    }
}

