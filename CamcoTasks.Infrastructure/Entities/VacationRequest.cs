// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TimeClock;

[Table("TimeClock_VacationsRequest")]
public class VacationRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("EmpId", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [Required]
    public string EmployeeId { get; set; }

    [Column("TotalHours", TypeName = "float")]
    public double? TotalHours { get; set; }

    [Column("IsApproved", TypeName = "bit")]
    public bool? IsApproved { get; set; }

    [Column("Reason", TypeName = "nvarchar(1024)")]
    [StringLength(1024)]
    [MaxLength(1024)]
    public string Reason { get; set; }

    [Column("ApprovedOrDisapprovedBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ApprovedOrDisapprovedBy { get; set; }

    [Column("FromDate", TypeName = "datetime")]
    public DateTime? FromDate { get; set; }

    [Column("ToDate", TypeName = "datetime")]
    public DateTime? ToDate { get; set; }

    [Column("IsSeen", TypeName = "bit")] public bool? IsSeen { get; set; }

    [Column("DisapprovedReason", TypeName = "nvarchar(1024)")]
    [StringLength(1024)]
    [MaxLength(1024)]
    public string DisapprovedReason { get; set; }

    [Column("UpdatedDate", TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    [Column("VacationId", TypeName = "int")]
    public int? VacationId { get; set; }

    [Column("IsDeleteOrEdit", TypeName = "nvarchar(10)")]
    [StringLength(10)]
    [MaxLength(10)]
    public string IsDeleteOrEdit { get; set; }

    [Column("RequestedDate", TypeName = "datetime")]
    public DateTime? RequestedDate { get; set; }

}