using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.Universities
{
    public class CreateUniversityValidator : AbstractValidator<CreateUniversityDto>
    //inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
    {
        public CreateUniversityValidator() //validator untuk create university
        {
            RuleFor(u => u.Code) //validator untuk properti code
               .NotEmpty() //tidak boleh kosong atau nol
               .MaximumLength(50); //max lenght inputan 50

            RuleFor(u => u.Name) //validator untuk properti name
               .NotEmpty() //tidak boleh kosong atau nol
               .MaximumLength(100); //max lenght inputan 100
        }
    }
}
