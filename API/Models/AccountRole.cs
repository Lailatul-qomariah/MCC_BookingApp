using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_account_roles")]
public class AccountRole : BaseEntity //implement dengan class abstract BaseEntity
{
    [Column("account_guid")]
    public Guid AccountGuid { get; set; }
    [Column("role_guid")]
    public Guid RoleGuid { get; set; }

    //cardinality dengan tbl account dan tbl role. account role sebagai one nya 
    public Account? Account { get; set; }
    public Role? Role { get; set; }

}