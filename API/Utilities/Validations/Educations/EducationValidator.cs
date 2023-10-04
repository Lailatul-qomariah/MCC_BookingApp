using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations;

public class EducationValidator : AbstractValidator<EducationDto>
//inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
{
    public EducationValidator() //validator untuk update education
    {
        RuleFor(e => e.Major) //validator untuk properti jurusan 
            .NotEmpty() //tidak boleh kosong atau nol
            .MaximumLength(100); //max panjang input karakter 100
        RuleFor(e => e.Degree) //validator untuk properti degree 
           .NotEmpty() //tidak boleh kosong atau nol
           .MaximumLength(100);
        RuleFor(e => e.Gpa) //validator untuk properti gpa 
           .NotEmpty() //tidak boleh kosong atau nol
           .InclusiveBetween(1, 4);
        RuleFor(e => e.UniversityGuid) //validator untuk properti univ guid 
           .NotEmpty(); //tidak boleh kosong atau nol
        RuleFor(e => e.Guid) //validator untuk properti guid 
           .NotEmpty(); //tidak boleh kosong atau nol
    }
}
