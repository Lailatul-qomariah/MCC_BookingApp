using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public abstract class BaseEntity 
{
    //class abstract agar bisa diimplementasikan disemua model yang membutuhkan properti dibawah ini
    [Key, Column("guid")]
    public Guid Guid { get; set; }
    
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }
}

