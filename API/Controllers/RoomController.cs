using API.Contracts;
using API.DTOs.Rooms;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class RoomController : ControllerBase
{
    //GENERIC
    //inheritance ke genericAllController
    /*public RoomController(IGenericRepository<Room> repositoryT) : base(repositoryT)
    {

    }*/



    //Non Generic
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

        var data = result.Select(x => (RoomDto)x);

        /*var universityDto = new List<UniversityDto>();
        foreach (var university in result)
        {
            universityDto.Add((UniversityDto) university);
        }*/

        return Ok(data);

    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roomRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok((RoomDto)result);
    }


    [HttpPost]
    public IActionResult Create(CreateRoomDto roomDto)
    {
        var result = _roomRepository.Create(roomDto);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }

        return Ok((RoomDto)result);
    }

    [HttpPut]
    public IActionResult Update(RoomDto roomDto)
    {
        var entity = _roomRepository.GetByGuid(roomDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }

        Room toUpdate = roomDto;
        toUpdate.CreatedDate = entity.CreatedDate;

        var result = _roomRepository.Update(toUpdate);
        if (!result)
        {
            return BadRequest("Failed to update data");
        }

        return Ok("Data Updated");
    }



    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        // Periksa apakah universitas dengan ID yang diberikan ada dalam database.
        var existingUniversity = _roomRepository.GetByGuid(guid);

        if (existingUniversity == null)
        {
            return NotFound("Data not found");
        }

        var deletedUniversity = _roomRepository.Delete(existingUniversity);

        if (deletedUniversity == null)
        {
            return BadRequest("Failed to delete data");
        }

        return Ok(deletedUniversity);
    }
}

