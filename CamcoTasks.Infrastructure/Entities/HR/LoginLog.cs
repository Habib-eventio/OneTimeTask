using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_LoginLogs")]
public class LoginLog
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("SignedInTime", TypeName = "datetime2(7)")]
    public DateTime SignedInTime { get; set; }

    [Column("SignedOutTime", TypeName = "datetime2(7)")]
    public DateTime? SignedOutTime { get; set; }

    [Column("TotalChanges", TypeName = "int")]
    public int? TotalChanges { get; set; }

    [Column("ApplicationId", TypeName = "smallint")]
    public short? ApplicationId { get; set; }

    public virtual ICollection<EmployeeViewLog> ViewedEmployees { get; set; } = new List<EmployeeViewLog>();

}