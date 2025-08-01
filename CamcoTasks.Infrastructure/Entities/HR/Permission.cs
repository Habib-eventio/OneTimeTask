using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Repository.HR;

[Table("HR_Permissions")]
public class Permission
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("JobId", TypeName = "bigint")]
    public long JobId { get; set; }

    [Column("PermissionActionId", TypeName = "int")]
    public int PermissionActionId { get; set; }

    [Column("FullAccess", TypeName = "bit")]
    public bool IsFullAccess { get; set; }

    [Column("ViewOnly", TypeName = "bit")] public bool IsViewOnly { get; set; }
}