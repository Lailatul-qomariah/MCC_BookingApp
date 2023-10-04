using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles
{
    public class RoleValidator : AbstractValidator<RoleDto>
    //inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
    {
        public RoleValidator() //validator untuk update role
        {
            RuleFor(r => r.Guid) //validator properti untuk guid
                .NotEmpty(); //tidak boleh kosong atau nol

            RuleFor(r => r.Name)  //validator properti untuk firstname
               .NotEmpty() //tidak boleh kosong atau nol
               .MaximumLength(100);  //max panjang lenght yang diinput 100 karakter

        }
    }
}
