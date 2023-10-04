using API.DTOs.Bookings;
using API.Models;
using API.Utilities.Enums;
using FluentValidation;
using System.Globalization;

namespace API.Utilities.Validations.Bookings;

public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
//inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
{
    public CreateBookingValidator() //validator untuk create booking
    {
        RuleFor(b => b.StartDate) //validator untuk properti start date
           .NotEmpty(); //tidak boleh kosong atau nol
        RuleFor(b => b.EndDate) //validator untuk properti end date
            .NotEmpty();//tidak boleh kosong atau nol
        RuleFor(b => b.Status) //validator untuk properti status
            .NotNull()//tidak boleh kosong atau nol
            .IsInEnum(); //data input harus enum antara 0 atau 1 dll
        RuleFor(b => b.Remarks) //validator untuk properti remarks
            .NotEmpty();
        RuleFor(b => b.RoomGuid) //validator untuk properti room guid
            .NotEmpty(); //tidak boleh kosong atau nol
        RuleFor(b => b.EmployeeGuid) //validator untuk properti employee guid
            .NotEmpty(); //tidak boleh kosong atau nol

    }
}
