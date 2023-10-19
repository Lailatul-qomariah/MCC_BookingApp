using API.DTOs.Employees;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Client.Controllers;
[Authorize(Roles = "admin, manager")]

public class EmployeeController : Controller // Inheritance ke class controller bawaan 
{
    private readonly IEmployeeRepository _repository; // Deklarasi variabel repository yang merupakan instance dari IEmployeeRepository.

    public EmployeeController(IEmployeeRepository repository) // Konstruktor EmployeeController dengan parameter repository.
    {
        _repository = repository; // Dependency injection
    }
    
    public async Task<IActionResult> List() // Metode List untuk get data employee.
    {
        // analogi 
        // "await" adalah cara kita memberi tahu program komputer untuk tidak menghentikan seluruh proses,
        // tetapi melanjutkan tugas lain(jika ada) sambil menunggu data(makanan) yang sedang diambil dari repository(dapur).
        var result = await _repository.Get(); // Memanggil metode Get() pada repository secara asinkron dan menyimpan hasilnya dalam variabel result.
        var listEmployee = new List<EmployeeDto>(); // Inisialisasi variabel listEmployee sebagai instance baru dari List<EmployeeDto>.

        if (result != null) // Memeriksa apakah hasil dari pemanggilan repository.Get() tidak null.
        {
      
            listEmployee = result.Data.Select(x => (EmployeeDto)x).ToList(); // Jika hasilnya tidak null, maka mengambil data dari result dan mengonversinya menjadi list employee.
        }

        return View(listEmployee); // Return view dengan parameter listEmployee.
    }

    [HttpGet]
    [Authorize(Roles = "admin")]

    public async Task<IActionResult> Details(Guid guid)
    {
        var result = await _repository.Get(guid);
        var employee = new EmployeeDto();
        if (result.Data?.Guid is null)
        {
            return View(employee);
        }
        return View(result.Data);
    }


    [HttpGet]
    public async Task<IActionResult> CreateEmp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmp(CreateEmployeeDto createEmployee)
    {
        if (ModelState.IsValid)
        {
            var result = await _repository.Post(createEmployee);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(List));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await _repository.Get(guid);
        var employee = new EmployeeDto();
        if (result.Data?.Guid is null)
        {
            return View(employee);
        }
        return View(result.Data);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(EmployeeDto employeeDto)
    {
        if (ModelState.IsValid)
        {
            var result = await _repository.Put(employeeDto.Guid, employeeDto);
            if (result != null)
            {
                if (result.Code == 200) // Perubahan berhasil
                {
                    return RedirectToAction(nameof(List));
                }
                else if (result.Code == 409) // Konflik, misalnya ada entitas dengan ID yang sama
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
                else
                {
                    // Handle status kode lain sesuai kebutuhan Anda
                    // Contoh:
                    ModelState.AddModelError(string.Empty, "Terjadi kesalahan saat menyimpan perubahan.");
                    return View();
                }
            }
            else
            {
                // Handle ketika result adalah null, misalnya ada kesalahan saat melakukan permintaan HTTP
                ModelState.AddModelError(string.Empty, "Terjadi kesalahan saat menyimpan perubahan.");
                return View();
            }
        }
        return View();
    }


    [HttpPost]

    /*[ValidateAntiForgeryToken]*/
    public async Task<IActionResult> RemoveEmployee(Guid Guid)
    {
        var result = await _repository.Delete(Guid);
        if (result.Code == 200)
        {
            return RedirectToAction(nameof(List));
        }
        return View();
    }

    [HttpPost]
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Employee()
    {
        return View();
    }
}

