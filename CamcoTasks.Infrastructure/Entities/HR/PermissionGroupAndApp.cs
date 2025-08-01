using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_PermissionGroupsAndApps")]
public class PermissionGroupAndApp
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("GroupId", TypeName = "int")] public int GroupId { get; set; }

    [Column("EnumAppId", TypeName = "smallint")]
    public short EnumAppId { get; set; }

    [Column("Role", TypeName = "nvarchar(256)")]
    [StringLength(256)]
    [MaxLength(256)]
    public string Role { get; set; }
}