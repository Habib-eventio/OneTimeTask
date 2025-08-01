// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TimeClock;

[Table("TimeClock_EmployeeSetting")]
public class EmployeeSetting
{
    [Column("Id", TypeName = "int")] [Key] public int Id { get; set; }

    [Column("EmpId", TypeName = "nvarchar(50)")]
    public string EmployeeId { get; set; }

    [Obsolete("This is Obsolete.")]
    [Browsable(false)]
    [Column("IsManager", TypeName = "bit")]
    public bool? IsManager { get; set; }

    [Column("IsActive", TypeName = "bit")] public bool? IsActive { get; set; }

    [Column("IsRemotelyWorking", TypeName = "bit")]
    public bool? IsRemotelyWorking { get; set; }

    [Column("ExpectedClockInTime", TypeName = "nvarchar(10)")]
    public string ExpectedClockInTime { get; set; }

    [Column("ExpectedClockOutTime", TypeName = "nvarchar(10)")]
    public string ExpectedClockOutTime { get; set; }

    [Obsolete("This is Obsolete.")]
    [Browsable(false)]
    [Column("EmailSentDate", TypeName = "datetime")]
    public DateTime? EmailSentDate { get; set; }

    [Column("CurrentTimeCategoryId", TypeName = "int")]
    public int? CurrentTimeCategoryId { get; set; }

    [Column("IsWorkingOnUpwork", TypeName = "bit")]
    public bool? IsWorkingOnUpwork { get; set; }

    [Column("ActivationDate", TypeName = "datetime")]
    public DateTime? ActivationDate { get; set; }
}