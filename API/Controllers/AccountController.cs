using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Principal;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmailHandler _emailHandler;

    //private Dictionary<string, (string otp, DateTime expiration)> otpStorage = new Dictionary<string, (string otp, DateTime expiration)>();


    //contructor dan dependency injection untuk menyimpan instance dari IAccountRepository
    public AccountController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IEmailHandler emailHandler)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _emailHandler = emailHandler;
    }

    [HttpGet] //menangani request get all data endpoint /Account
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Account dari _accountRepository.
        var result = _accountRepository.GetAll();
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
        // mengubah data Account ke dalam format DTO secara explicit.
        var data = result.Select(x => (AccountDto)x);
        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Account/{guid}
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _accountRepository.GetByGuid(guid);
        // cek apakah data result kosong atau apakah data berdasarkan guid ditemukan
        if (result is null)
        {
            return NotFound("Id Not Found"); //return Notfound jika data tidak ditemukan
        }
        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
    }

    [HttpPost] //menangani request create data ke endpoint /Account
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    public IActionResult Create(CreateAccountDto accountDto)
    {
        try
        {
            Account toCreate = accountDto;
            toCreate.Password = HashHandler.HashPassword(accountDto.Password);
            // create data university menggunakan format data DTO implisit
            var result = _accountRepository.Create(toCreate);

            //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
            return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
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
        }


    }

    [HttpPut] //menangani request update ke endpoint /Account
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    public IActionResult Update(AccountDto accountDto)
    {
        try
        {
            //get data by guid dan menggunakan format DTO 
            var entity = _accountRepository.GetByGuid(accountDto.Guid);
            if (entity is null) //cek apakah data berdasarkan guid tersedia 
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            //convert data DTO dari inputan user menjadi objek Account
            Account toUpdate = accountDto;
            //menyimpan createdate yg lama 
            toUpdate.CreatedDate = entity.CreatedDate;
            toUpdate.Password = HashHandler.HashPassword(accountDto.Password);

            //update Account dalam repository
            _accountRepository.Update(toUpdate);

            // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
            return Ok(new ResponseOKHandler<string>("Data Updated"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Update data",
                Error = ex.Message
            });
        }
    }

    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Account
    public IActionResult Delete(Guid guid)
    {

        try
        {
            // get data account by guid
            var entity = _accountRepository.GetByGuid(guid);
            // cek apakah entity (get data by guid) kosong
            if (entity is null)
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            //delete Account dari repository
            _accountRepository.Delete(entity);

            // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
            return Ok(new ResponseOKHandler<string>("Data Deleted"));

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Delete data",
                Error = ex.Message
            });
        }



    }

    [HttpPut("ForgotPassWord")]
    public IActionResult ForgotPasword(string emailForgot)
    {

        var employee = _employeeRepository.GetAll();
        var account = _accountRepository.GetByEmail(emailForgot); // get account berdasarkan email

        if (!(employee.Any() && account != null))
        {
            // Respons dengan kode status HTTP 404 (Not Found) dengan pesan kesalahan yang dihasilkan.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        
        int otp = GenerateHandler.GenerateOtp();
        account.Otp = otp;
        account.IsUsed = false;
        account.ExpiredTime = DateTime.Now.AddMinutes(5);
        _accountRepository.Update(account);
        var accAll = _accountRepository.GetAll();

        _emailHandler.Send("Forgot Password", $"Your OTP is {account.Otp}", emailForgot);
        var accountView = from emp in employee
                          join acc in accAll on emp.Guid equals acc.Guid
                          select new AccountForgotPaswordDto
                          {
                              Otp = acc.Otp,
                              ExpireTime = acc.ExpiredTime
                          };

        return Ok(new ResponseOKHandler<IEnumerable<AccountForgotPaswordDto>>(accountView));
    }



    [HttpPut("ChangePassword")]
    public IActionResult ChangePassword(AccountChangePasswordDto changePsswd)
    {
        // get account  berdasarkan alamat email
        var account = _accountRepository.GetByEmail(changePsswd.Email);

        if (account == null)
        {
            // Respons dengan kode status HTTP 404 (Not Found) jika akun tidak ditemukan.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account Not Found"
            });
        }

        // Periksa apakah OTP yang dimasukkan oleh pengguna sesuai dengan yang ada dalam akun
        if (changePsswd.Otp != account.Otp)
        {
            // Respons dengan pesan kesalahan jika OTP tidak cocok
            return BadRequest(new ResponseErrorHandler
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Invalid OTP"
            });
        }

        // Periksa apakah OTP sudah digunakan sebelumnya
        if (account.IsUsed)
        {
            // Respons dengan pesan kesalahan jika OTP sudah digunakan sebelumnya
            return BadRequest(new ResponseErrorHandler
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "OTP has already been used"
            });
        }

        // Periksa apakah OTP sudah kedaluwarsa
        var currentTime = DateTime.UtcNow;
        var expiredTime = account.ExpiredTime;

        if (currentTime > expiredTime)
        {
            // Respons dengan pesan kesalahan jika OTP sudah kedaluwarsa
            return BadRequest(new ResponseErrorHandler
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "OTP has expired"
            });
        }

        // Periksa apakah NewPassword sama dengan ConfirmPassword
        if (changePsswd.NewPassword != changePsswd.ConfirmPassword)
        {
            // Respons dengan pesan kesalahan jika NewPassword tidak cocok dengan ConfirmPassword
            return BadRequest(new ResponseErrorHandler
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "NewPassword and ConfirmPassword do not match"
            });
        }

        ////kurang update nya ke db
        int otp = GenerateHandler.GenerateOtp();
        account.Otp = otp;
        account.IsUsed = true;
        account.ExpiredTime = DateTime.Now.AddMinutes(5);
        _accountRepository.Update(account);
        return Ok(new ResponseOKHandler<string>("Password changed successfully"));
    }

    [HttpPost("Login")]
    public IActionResult Login(LoginDto loginDto)
    {
        try
        {
            var account = _accountRepository.GetByEmail(loginDto.Email);

            //bool hashedPassword = HashHandler.VerifyPassword(loginDto.Password, account.Password);
            if (account == null)
            {
                // Respons dengan kode status HTTP 404 (Not Found) jika akun tidak ditemukan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account Not Found"
                });
            }

            // Periksa apakah kata sandi yang dihash cocok dengan kata sandi di database
            if (HashHandler.VerifyPassword(loginDto.Password, account.Password))
            {
                // Respons dengan pesan kesalahan jika kata sandi salah
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Account or Password is invalid"
                });
            }
                return Ok(new ResponseOKHandler<string>("Login Berhasil"));
            
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseErrorHandler
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Account or Password is invalid",
                Error = ex.Message.ToString()
            }); ;
        }
        

        // Jika verifikasi kata sandi berhasil, Anda memberikan respons sukses


    }



}










