using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_rooms")]
public class Room : BaseEntity //implement dengan class abstract BaseEntity
{
    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; }
    
    [Column("floor")]
    public int Floor { get; set; }
  
    [Column("capacity")]
    public int Capacity { get; set; }
    public ICollection<Booking>? Booking { get; set; } //cardinalitasnya many

}