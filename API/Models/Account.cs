using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_accounts")]

public class Account : BaseEntity
{
    [Column("password", TypeName = "nvarchar(max)")]
    public string Password { get; set; }
    
    [Column("otp")]
    public int Otp { get; set; }
    
    [Column ("is_used")]
    public bool IsUsed { get; set; }
    
    [Column ("expired_time")]
    public DateTime ExpiredTime { get; set; }
    public Employee? Employee { get; set; }

    //yang memiliki ICollection adalah tabel one nya, kalo yang many tinggal manggil nama kelasnya 
    public ICollection<AccountRole>? AccountRole { get; set; }

}