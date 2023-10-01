using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class BookingController : GenericAllController<Booking>
{

    public BookingController(IAllRepository<Booking> repositoryT) : base(repositoryT)
    {
        
    }

    //Non Generic
    private readonly IBookingRepository _bookingRepository;

    public BookingController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _bookingRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _bookingRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Booking booking)
    {
        var result = _bookingRepository.Create(booking);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] Booking booking)
    {
        var existingBooking = _bookingRepository.GetByGuid(guid);

        if (existingBooking == null)
        {
            return NotFound("Data not found");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //booking
        existingBooking.Remarks = booking.Remarks; //update code dengan code dari inputan

        var updatedBooking = _bookingRepository.Update(existingBooking);

        if (updatedBooking == null)
        {
            return BadRequest("Failed to update data");
        }

        return Ok(updatedBooking);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingBooking = _bookingRepository.GetByGuid(guid);

        if (existingBooking == null)
        {
            return NotFound("Role not found");
        }

        var deletedBooking = _bookingRepository.Delete(existingBooking);

        if (deletedBooking == null)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok(deletedBooking);
    }


}

