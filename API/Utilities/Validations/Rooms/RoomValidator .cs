using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms;

public class RoomValidator : AbstractValidator<RoomDto>
//inheritance ke AbstractValidator agar bisa akses method dll untuk validation di FluentValidation
{
    public RoomValidator() //validator untuk update room
    {
        RuleFor(r => r.Name) //validator untuk properti name
            .NotEmpty() //tidak boleh kosong atau nol
            .MaximumLength(100);
        RuleFor(r => r.Floor) //validator untuk floor
            .NotNull(); //tidak boleh kosong atau nol
        RuleFor(r => r.Capacity) //validator untuk capacity
            .NotEmpty(); //tidak boleh kosong atau nol
        RuleFor(r => r.Guid) //validator properti untuk guid
           .NotEmpty(); //tidak boleh kosong atau nol

    }


    
}
