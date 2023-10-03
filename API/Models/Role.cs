using System.ComponentModel.DataAnnotations.Schema;


namespace API.Models;
[Table("tb_m_roles")]

public class Role : BaseEntity //implement dengan class abstract BaseEntity
{
    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; }
    public ICollection<AccountRole>? AccountRole { get; set; } //cardinalitynya many

}