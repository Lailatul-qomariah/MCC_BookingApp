using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class RoomController : GenericAllController<Room>
{

    public RoomController(IAllRepository<Room> repositoryT) : base(repositoryT)
    {

    }



  /*  //Non Generic
    private readonly IRoomRepository _roomRepository;

    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roomRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roomRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Room room)
    {
        var result = _roomRepository.Create(room);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok(result);
    }

    [HttpPut("{guid}")]
    public IActionResult Update(Guid guid, [FromBody] Room room)
    {
        var existingRoom = _roomRepository.GetByGuid(guid);

        if (existingRoom == null)
        {
            return NotFound("University not found");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //example
        existingRoom.Code = university.Code; //update code dengan code dari inputan
        existingRoom.Name = university.Name; //update name dengan name baru yang ada di inputan

        var updatedRoom = _roomRepository.Update(existingRoom);

        if (updatedRoom == null)
        {
            return BadRequest("Failed to update Room");
        }

        return Ok(updatedRoom);
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingRoom = _roomRepository.GetByGuid(guid);

        if (existingRoom == null)
        {
            return NotFound("Room not found");
        }

        var deletedRoom = _roomRepository.Delete(existingRoom);

        if (deletedRoom == null)
        {
            return BadRequest("Failed to delete university");
        }

        return Ok(deletedRoom);
    }*/

}

