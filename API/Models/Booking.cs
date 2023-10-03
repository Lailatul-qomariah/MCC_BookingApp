using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;


[Table("tb_tr_bookings")]
public class Booking : BaseEntity //implement dengan class abstract BaseEntity
{
    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime EndDate { get; set; }

    [Column("status")]
    public StatusLevels Status { get; set; }

    [Column("remarks", TypeName = "nvarchar(max)")]
    public string Remarks { get; set; }

    [Column("room_guid")]
    public Guid RoomGuid { get; set; }

    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    //employee dan room cardinalitunya one
    public Employee? Employee { get; set; }

    public Room? Room { get; set; }

}