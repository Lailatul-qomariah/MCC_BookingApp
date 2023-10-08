using API.Contracts;
using API.DTOs.Bookings;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "manager, admin")]
public class BookingController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IEmployeeRepository _employeeRepository;

    //contructor dan dependency injection untuk menyimpan instance dari IBookingRepository
    public BookingController(IBookingRepository bookingRepository, IRoomRepository roomRepository,
        IEmployeeRepository employeeRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _employeeRepository = employeeRepository;
    }
    
    //soal no 4
    [HttpGet("BookingDuration")]
    [Authorize(Roles = "manager")]
    public IActionResult GetBookingLength()
    {
        try
        {
            // get all data dari tabel Booking dan Room
            var booking = _bookingRepository.GetAll();
            var room = _roomRepository.GetAll();

            // inisiasi day off yang tidak masuk perhitungan yaitu Sabtu dan Minggu
            var offDay = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

            // membuat list objek BookingLengthDto yang akan menampung result perhitungan
            var roomBookingLengths = new List<BookingLengthDto>();
            foreach (var r in room) //perulangan untuk setiap room
            {
                // get semua data booking untuk masing-masing ruangan
                var roomBookings = booking
                    .Where(b => b.RoomGuid == r.Guid);
                if (roomBookings.Any()) //cek apakah roombooking memiliki data
                {
                    //inisiasi variabel utuk perhitungan
                    var bookingDurationInHours = 0;

                    // looping untuk setiap data booking pada room
                    foreach (var b in roomBookings)
                    {
                        var startDate = b.StartDate;
                        var endDate = b.EndDate;

                        //looping untuk menghitung jumlah jam kerja antara start date dan end date booking
                        while (startDate <= endDate)
                        {
                            //cek apakah tanggal saat ini bukan day off 
                            if (!offDay.Contains(startDate.DayOfWeek))
                            {
                                bookingDurationInHours += 1; // Menambahkan 1 jam ke total durasi booking
                            }
                            startDate = startDate.AddHours(1); // Menambahkan 1 jam ke waktu mulai
                        }
                    }
                    var durationBookingInDays= bookingDurationInHours / 24;  //menghitung total durasi booking dalam jumlah hari
                    var remainingHours = bookingDurationInHours % 24;  //menghitung sisa jam booking setelah menghitung dalam hari

                    // menambahkan hasil perhitungan ke list roomBookingLengths dalam bentuk objek
                    roomBookingLengths.Add(new BookingLengthDto
                    {
                        RoomGuid = r.Guid,
                        RoomName = r.Name,
                        BookingLength = $"{durationBookingInDays} Hari {remainingHours} Jam"
                    });
                }
            }

            // return daftar hasil perhitungan dalam respons OK
            return Ok(new ResponseOKHandler<IEnumerable<BookingLengthDto>>(roomBookingLengths));

        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi exception saat mengambil data, akan mengembalikan respon handler 500 dengan pesan exception.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to calculate booking lengths",
                Error = ex.Message
            });
        }
    }


    //jawaban soal no 2
    [HttpGet("details")]
    [Authorize(Roles = "manager")]
    public IActionResult GetAllDetails()
    {
        var booking = _bookingRepository.GetAll(); //get data dari tabel booking 
        var room = _roomRepository.GetAll(); //get data dari tabel room 
        var employee = _employeeRepository.GetAll(); //get data dari tabel employee 
        // cek apakah ada data booking, data room, dan data employee tersedia
        if (!(booking.Any() && room.Any() && employee.Any()))
        {
            //jika data tidak ditemukan maka akan masuk ke handler error 404 notfound
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        //join 3 objek untuk mengambil data sesuai kebutuhan berdasarkan format BookingDetailsDto
        var bookingDetails = from r in room
                             join b in booking on r.Guid equals b.RoomGuid
                             join e in employee on b.EmployeeGuid equals e.Guid
                             //instance untuk mengisi properti dalam BookingDetailsDto berdasarkan objek employee, room dan booking 
                             select new BookingDetailsDto
                             {
                                 Guid = b.Guid, // set properti GUID di BookingDetailsDto dengan GUID dari booking
                                 BookedNIK = e.Nik, //set properti NIK di BookingDetailsDto dengan NIK dari employee
                                 BookedBy = string.Concat(e.FirstName, " ", e.LastName), //concat menggabungkan firt dan last name
                                 RoomName = r.Name, //set properti RoomName di BookingDetailsDto dengan Name dari room
                                 StartDate = b.StartDate,
                                 EndDate = b.EndDate,
                                 Status = b.Status.ToString(),//convert Status dalam bentuk enum ke string contoh  0 menjadi "Ongoing"
                                 Remarks = b.Remarks
                             };
        return Ok(new ResponseOKHandler<IEnumerable<BookingDetailsDto>>(bookingDetails));
    }

    [HttpGet("detail{guid}")]
    [Authorize(Roles = "user")]
    public IActionResult GetDetailsByGuid(Guid guid)
    {
        // get data booking berdasarkan guid yang diinput
        var booking = _bookingRepository.GetByGuid(guid);

        if (booking == null) //cek apakah data booking kosong
        {
            //jika data tidak ditemukan maka akan masuk ke handler error 404 notfound
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // get data employee dan room berdasarkan guid yang ada di booking
        var employee = _employeeRepository.GetByGuid(booking.EmployeeGuid);
        var room = _roomRepository.GetByGuid(booking.RoomGuid);

        // Membuat instance BookingDetailsDto berdasarkan data booking, employee, dan room
        var bookingDetails = new BookingDetailsDto
        {
            Guid = booking.Guid, // set properti GUID di BookingDetailsDto dengan GUID dari booking
            BookedNIK = employee.Nik, //set properti NIK di BookingDetailsDto dengan NIK dari employee
            BookedBy = string.Concat(employee.FirstName, " ", employee.LastName),
            RoomName = room.Name, //set properti RoomName di BookingDetailsDto dengan Name dari room
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status.ToString(), //convert Status dalam bentuk enum ke string contoh  0 menjadi "Ongoing"
            Remarks = booking.Remarks
        };

        return Ok(new ResponseOKHandler<BookingDetailsDto>(bookingDetails));
    }

    [HttpGet] //menangani request get all data endpoint /Booking
    public IActionResult GetAll()
    {
        // Mendapatkan semua data Booking dari _bookingRepository.
        var result = _bookingRepository.GetAll();
        if (!result.Any()) //cek apakah data ditemukan
        {
            //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        // mengubah data Booking ke dalam format DTO secara explicit.
        var data = result.Select(x => (BookingDto)x);

        //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
        return Ok(new ResponseOKHandler<IEnumerable<BookingDto>>(data));

    }

    [HttpGet("{guid}")] //menangani request get data by guid endpoint /Booking/{guid}
    public IActionResult GetByGuid(Guid guid)
    {
        //get data berdasarkan guid yang diinputkan user
        var result = _bookingRepository.GetByGuid(guid);
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
        return Ok(new ResponseOKHandler<BookingDto>((BookingDto)result));
    }

    [HttpPost] //menangani request create data ke endpoint /Booking
    //parameter berupa objek menggunakan format DTO agar crete data disesuaikan dengan format DTO
    [Authorize(Roles = "user")]
    public IActionResult Create(CreateBookingDto bookingDto)
    {
        try
        {
            // create data Booking menggunakan format data DTO implisit
            var result = _bookingRepository.Create(bookingDto);

            //return HTTP OK dan data dalam format DTO dengan kode status 200 untuk success
            return Ok(new ResponseOKHandler<BookingDto>((BookingDto)result));

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

    [HttpPut] //menangani request update ke endpoint /Booking
    //parameter berupa objek menggunakan format DTO explicit agar crete data disesuaikan dengan format DTO
    [Authorize(Roles = "user")]
    public IActionResult Update(BookingDto bookingDto)
    {
        try
        {
            //get data by guid dan menggunakan format DTO 
            var entity = _bookingRepository.GetByGuid(bookingDto.Guid);
            if (entity is null) //cek apakah data berdasarkan guid tersedia 
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            //convert data DTO dari inputan user menjadi objek Booking
            Booking toUpdate = bookingDto;
            //menyimpan createdate yg lama 
            toUpdate.CreatedDate = entity.CreatedDate;

            //update Booking dalam repository
            _bookingRepository.Update(toUpdate);

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

    [HttpDelete("{guid}")] //menangani request delete ke endpoint /Booking
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // get data booking by guid
            var entity = _bookingRepository.GetByGuid(guid);

            // cek apakah entity (get data by guid) kosong
            if (entity == null)
            {
                //respons dengan kode status HTTP 404(Not Found) dengan pesan kesalahan yang dihasilkan.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            //delete Booking dari repository
            _bookingRepository.Delete(entity);

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

