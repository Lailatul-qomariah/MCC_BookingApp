using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;
public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDto>
//inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
{
    public CreateAccountRoleValidator() //validator untuk create account role
    {
        RuleFor(a => a.AccountGuid) // validator untuk guid account guid
           .NotEmpty(); //tidak boleh kosong atau nol
        RuleFor(a => a.RoleGuid) // validator untuk role guid
            .NotEmpty(); //tidak boleh kosong atau nol
    }
}
