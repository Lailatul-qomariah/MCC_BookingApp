using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;
public class AccountRoleValidator : AbstractValidator<AccountRoleDto>
//inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
{
    public AccountRoleValidator() // validator untuk update Account role
    {
        RuleFor(a => a.Guid) // validator untuk guid account role
           .NotEmpty(); //tidak boleh kosong atau nol
        RuleFor(a => a.AccountGuid) // validator untuk guid account guid
            .NotEmpty(); //tidak boleh kosong atau nol
        RuleFor(a => a.RoleGuid) // validator untuk role guid
            .NotEmpty(); //tidak boleh kosong atau nol
    }
}
