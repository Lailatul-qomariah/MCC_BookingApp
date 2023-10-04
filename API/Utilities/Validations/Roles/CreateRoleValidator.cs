using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
//inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
{
    public CreateRoleValidator() //validator untuk create role
    {
        RuleFor(r => r.Name)  //validator properti untuk firstname
            .NotEmpty() //tidak boleh kosong atau nol
            .MaximumLength(100);  //max panjang lenght yang diinput 100 karakter
    }
}

