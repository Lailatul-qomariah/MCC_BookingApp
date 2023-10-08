using API.Contracts;
using API.DTOs.Rooms;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "admin")]

public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeRepository _employeeRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IRoomRepository
    public RoomController(IRoomRepository roomRepository, IBookingRepository bookingRepository,
        IEmployeeRepository employeeRepository)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
        _employeeRepository = employeeRepository;
    }

    //soal no 3
    [HttpGet("RoomAvailable")]
    [Authorize(Roles = "user, manager")]
    public IActionResult GetAvailableRooms()
    {
        // get semua data dari tabel Room
        var room = _roomRepository.GetAll();

        // get semua data dari tabel Booking
        var booking = _bookingRepository.GetAll();

        // ce apakah tidak ada data room dan booking
        if (!(room.Any() && booking.Any()))
        {
            // Jika tidak ada data room dan booking, maka return respons HTTP 404 Not Found
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // get date hari ini
        DateTime today = DateTime.Now.Date;

        // get daftar room yang sedang digunakan hari ini
        var usedRoom = booking
            .Where(b => b.StartDate.Date <= today && today <= b.EndDate.Date && b.Status == Utilities.Enums.StatusLevels.OnGoing)
            .Select(b => b.RoomGuid).Distinct().ToList();

        // get seluruh daftar room yang tidak digunakan maupun yang belum pernah dibooking
        var availableRooms = room.Where(r => !usedRoom.Contains(r.Guid)).Select(r => new RoomDto
        {
            Guid = r.Guid,
            Name = r.Name,
            Floor = r.Floor,
            Capacity = r.Capacity
        }).ToList(); //convert objek kedalam bentuk list

        // cek apakah ada ruangan yang tersedia
        if (!availableRooms.Any())
        {
            // Jika tidak ada ruangan yang tersedia, return respons HTTP 404 Not Found
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Tidak ada ruangan yang tersedia hari ini"
            });
        }

        // return room yang tersedia dalam respons OK
        return Ok(new ResponseOKHandler<IEnumerable<RoomDto>>(availableRooms));
    }

    //soal no 1
    [HttpGet("RoomInUse")]
    [Authorize(Roles = "manager")]
    public IActionResult GetInUse()
    {
        var room = _roomRepository.GetAll(); // get all data room
        var booking = _bookingRepository.GetAll(); //get all data booking
        var employee = _employeeRepository.GetAll(); //get all data employee

        if (!room.Any() && booking.Any() && employee.Any())//cek apakah data ditemukan
        {
            //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        DateTime today = DateTime.Now.Date; //get tanggal hari ini 
        var roomInUse = from r in room
                        join b in booking on r.Guid equals b.RoomGuid
                        join e in employee on b.EmployeeGuid equals e.Guid
                        where b.Status == Utilities.Enums.StatusLevels.OnGoing && //where status roomnya ongoing / sedang dipakai
                        b.StartDate.Date <= today && today <= b.EndDate.Date //where tanggalnya untuk penggunaan room hari ini 
                        //instance untuk mengisi properti dalam InUseRoomDto berdasarkan objek room dan booking 
                        select new InUseRoomDto
                        {
                            BookingGuid = b.Guid,
                            RoomName = r.Name,
                            Status = b.Status.ToString(),
                            Floor = r.Floor,
                            BookedBy = string.Concat(e.FirstName, " ", e.LastName)
                        };
        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<IEnumerable<InUseRoomDto>>(roomInUse));
    }

    [HttpGet] //menangani request get all data endpoint /Room
    [Authorize(Roles = "manager")]
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Room dari _roomRepository.
        var result = _roomRepository.GetAll();
        if (!result.Any())//cek apakah data ditemukan
        {
            //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        // mengubah data Room ke dalam format DTO secara explicit.
        var data = result.Select(x => (RoomDto)x);
        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<IEnumerable<RoomDto>>(data));

    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Room/{guid}
    [Authorize(Roles = "manager")]
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _roomRepository.GetByGuid(guid);
        // cek apakah data result kosong atau apakah data berdasarkan guid ditemukan
        if (result is null)
        {
            //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<RoomDto>((RoomDto)result));
    }


    [HttpPost] //menangani request create data ke endpoint /Room
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    [Authorize(Roles = "manager")]
    public IActionResult Create(CreateRoomDto roomDto)
    {
        try
        {
            // create data Room menggunakan format data DTO implisit
            var result = _roomRepository.Create(roomDto);

            //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
            return Ok(new ResponseOKHandler<RoomDto>((RoomDto)result));
        }
        catch (Exception ex)
        {
            // return dengan kode status 500 dan menampilkan pesan exception 
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to create data",
                Error = ex.Message
            });
        }

    }

    [HttpPut] //menangani request update ke endpoint /Room
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    [Authorize(Roles = "manager")]
    public IActionResult Update(RoomDto roomDto)
    {
        try
        {
            //get data by guid dan menggunakan format DTO 
            var entity = _roomRepository.GetByGuid(roomDto.Guid);
            if (entity is null)//cek apakah data berdasarkan guid tersedia 
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            //convert data DTO dari inputan user menjadi objek Room
            Room toUpdate = roomDto;
            //menyimpan createdate yg lama 
            toUpdate.CreatedDate = entity.CreatedDate;
            //update Room dalam repository
            _roomRepository.Update(toUpdate);
            // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
            return Ok(new ResponseOKHandler<string>("Data Updated"));
        }
        catch (Exception ex)
        {
            // return dengan kode status 500 dan menampilkan pesan exception 
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Update data",
                Error = ex.Message
            });
        }

    }

    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Room
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // get data room by guid
            var existingRoom = _roomRepository.GetByGuid(guid);

            // cek apakah existingRoom (get data by guid) kosong
            if (existingRoom == null)
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            //delete Room dari repository
            _roomRepository.Delete(existingRoom);

            // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
            return Ok(new ResponseOKHandler<string>("Data Deleted"));
        }
        catch (Exception ex)
        {
            // return dengan kode status 500 dan menampilkan pesan exception 
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Delete data",
                Error = ex.Message
            });
        }


    }

}

