using API.Models;

namespace API.DTOs.Accounts;

public class CreateAccountDto
// Representasi DTO untuk membuat entitas Account
{
    //properti Account yang akan dibuat
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }

    // Operator implisit untuk mengubah objek CreateAccountDto menjadi objek Account
    public static implicit operator Account(CreateAccountDto createAccountDto)
    {

        // Inisiasi objek Account dengan data dari objek CreateAccountDto
        return new Account

        {
            Guid = createAccountDto.Guid,
            Password = createAccountDto.Password,
            Otp = createAccountDto.Otp,
            IsUsed = createAccountDto.IsUsed,
            ExpiredTime = createAccountDto.ExpiredTime,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
