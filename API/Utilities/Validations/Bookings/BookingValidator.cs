using API.DTOs.Bookings;
using FluentValidation;

namespace API.Utilities.Validations.Bookings;

public class BookingValidator : AbstractValidator<BookingDto>
//inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
{
    public BookingValidator() //validator untuk update booking
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
        RuleFor(b => b.Guid) //validator untuk properti guid
            .NotEmpty(); //tidak boleh kosong atau nol

    }
}
