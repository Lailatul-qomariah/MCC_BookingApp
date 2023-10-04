using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees;
public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
//inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
{
    public CreateEmployeeValidator() //validator untuk create employee
    {
        RuleFor(e => e.FirstName)   //validator untuk firstname
            .NotEmpty() //tidak boleh kosong atau nol
            .MaximumLength(100); //max panjang karakter yg diinputkan adalah 100

        RuleFor(e => e.BirthDate) //validator untuk firstname
           .NotEmpty() //tidak boleh kosong atau 0
           .GreaterThanOrEqualTo(DateTime.Now.AddYears(-18)); // max batas usia adalah 18 tahun

        RuleFor(e => e.Gender) //validator untuk properti gender 
           .NotNull() //tidak boleh kosong atau nol
           .IsInEnum(); //untuk enum antara 0 atau 1 

        RuleFor(e => e.HiringDate) //validator untuk properti hiring date 
            .NotEmpty(); //tidak boleh kosong atau nol

        RuleFor(e => e.Email) //validator untuk properti email 
           .NotEmpty() //tidak boleh kosong atau nol
           .EmailAddress().WithMessage("Format Email Salah"); //mengatur agar sesuai format email

        RuleFor(e => e.PhoneNumber) //validator untuk properti phone number 
           .NotEmpty() //tidak boleh kosong atau nol
           .MinimumLength(10) //minimun panjang inputan 
           .MaximumLength(20) //minimun panjang inputan 
           .Matches(@"^[0-9]+$") // menggunakan pattern untuk memeriksa input hanya angka
           .WithMessage("Nomor telepon harus berisi hanya angka."); //

    }
}

