using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class RegistrationValidator : AbstractValidator<RegisterAccountDto> 
    {
        public RegistrationValidator() 
        {

            RuleFor(a => a.Password) //set validation untuk properti password
                .NotEmpty() //tidak boleh kosong atau 0
                .MinimumLength(8) //min lenght karakter 8
                .MaximumLength(16) //max lenght karakter 16
                .Matches(@"[A-Z]+") //harus berisi min 1 huruf kapital
                .Matches(@"[a-z]+") //harus berisi min 1 huruf lowercase
                .Matches(@"[0-9]+") //harus berisi min 1 angka
                .Matches(@"[\!\?\*\.]+"); //harus berisi min 1 karakter
            RuleFor(a => a.ConfirmPassword) //set validation untuk properti confirmpassword
                //masing-masing penjelasan baris nya sama seperti yang diatas
                .NotEmpty() 
                .MinimumLength(8)
                .MaximumLength(16)
                .Matches(@"[A-Z]+")
                .Matches(@"[a-z]+")
                .Matches(@"[0-9]+")
                .Matches(@"[\!\?\*\.]+");
        }
    }
}
