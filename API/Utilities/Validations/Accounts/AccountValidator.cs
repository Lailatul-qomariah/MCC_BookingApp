using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class AccountValidator : AbstractValidator<AccountDto>
    { 
        public AccountValidator()
        {
            RuleFor(a => a.Guid) //select guid, validator untuk guid
                .NotEmpty(); //tidak boleh kosong atau nol 
            RuleFor(a => a.Password) //validator untuk password 
                .NotEmpty() //tidak boleh kosong atau nol 
                .MinimumLength(8); //minimal panjang karakter 8
            RuleFor(a => a.Otp) //validator untuk properti Otp
                .NotEmpty(); //tidak boleh kosong atau nol 
            RuleFor(a => a.IsUsed) //validator untuk properti IsUsed
                .NotEmpty(); //tidak boleh kosong atau nol 
            RuleFor(a=> a.ExpiredTime) //validator untuk expired time 
                .NotEmpty() //tidak boleh kosong atau nol 
                .Must(expiredTime => expiredTime > DateTime.Now); //expired time harus lebih dari tanggal hari ini 
        }
    }
}
