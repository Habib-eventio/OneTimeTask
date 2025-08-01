using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_PermissionGroupsAndUsers")]
public class PermissionGroupAndUser
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("GroupId", TypeName = "int")] public int GroupId { get; set; }

    [Browsable(false)]
    [Column("UserId", TypeName = "varchar(MAX)")]
    [MaxLength]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUserIdEmployeeId Column). Please refer to CorrectUserIdEmployeeId")]
    public string ObsoleteUserId { get; set; }

    [Column("CorrectUserIdEmployeeId", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string UserEmployeesId { get; set; }
}