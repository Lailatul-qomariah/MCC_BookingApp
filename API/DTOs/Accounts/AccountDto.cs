using API.Models;

namespace API.DTOs.Accounts;

public class AccountDto
{
    //representasi DTO untuk model atau entitaas Account
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }


    // Operator eksplisit untuk convert objek Account ke AccountDto
    //digunakan atau dipanggil pada method GetAll, GetByGuid dan Create di controller
    public static explicit operator AccountDto(Account account)
    {
        // Inisiasi objek UniversityDto dengan data dari objek Account
        return new AccountDto
        {
            Guid = account.Guid,
            Password = account.Password,
            Otp = account.Otp,
            IsUsed = account.IsUsed

        };
    }

    // Operator implisit untuk convert objek AccountDto ke Account
    //digunakan pada saat menggunakan method Update di controller
    public static implicit operator Account(AccountDto accountDto)
    {
        // Inisiasi objek Account dengan data dari objek AccountDto
        return new Account
        {
            Guid = accountDto.Guid,
            Password = accountDto.Password,
            IsUsed = accountDto.IsUsed,
            ExpiredTime = accountDto.ExpiredTime,
            ModifiedDate = DateTime.Now
        };
    }
}
