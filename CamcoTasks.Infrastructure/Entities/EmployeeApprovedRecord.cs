// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TimeClock;

[Table("TimeClock_EmployeeApprovedRecords")]
public class EmployeeApprovedRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("EmpId", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string EmployeeId { get; set; }

    [Column("StartDate", TypeName = "date")]
    public DateTime? StartDate { get; set; }

    [Column("EndDate", TypeName = "date")] public DateTime? EndDate { get; set; }

    [Column("IsApproved", TypeName = "bit")]
    public bool? IsApproved { get; set; }
}