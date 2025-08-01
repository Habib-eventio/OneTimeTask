using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_EmployeeViewLogs")]
public class EmployeeViewLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "bigint")]
    public long Id { get; set; }

    [Column("ViewedEmployeeId", TypeName = "bigint")]
    public long ViewedEmployeeId { get; set; }

    [Column("ViewedAt", TypeName = "datetime2(7)")]
    public DateTime ViewedAt { get; set; }

    [ForeignKey("LoginLog")]
    [Column("LoginLogId", TypeName = "bigint")]
    public long LoginLogId { get; set; }

    public LoginLog LoginLog { get; set; }
}