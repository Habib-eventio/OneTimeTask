// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TimeClock;

[Table("TimeClock_BusinessTimeRequests")]
public class BusinessTimeRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("IsApproved", TypeName = "bit")]
    public bool? IsApproved { get; set; }

    [Column("ApprovedOrDisapprovedDate", TypeName = "datetime")]
    public DateTime? ApprovedOrDisapprovedDate { get; set; }

    [Column("ApprovedOrDisapprovedBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ApprovedOrDisapprovedBy { get; set; }

    [Column("CheckInOutId", TypeName = "int")]
    public int? CheckInOutId { get; set; }

    [Column("BusinessTimeId", TypeName = "int")]
    public int? BusinessTimeId { get; set; }

    [Column("EmpId", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string EmployeeId { get; set; }

    [Column("RequestedLeftTime", TypeName = "datetime")]
    public DateTime? RequestedLeftTime { get; set; }

    [Column("RequestedReturnedTime", TypeName = "datetime")]
    public DateTime? RequestedReturnedTime { get; set; }

    [Column("PlaceOfGoing", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PlaceOfGoing { get; set; }

    [Column("DisapprovedReason", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string DisapprovedReason { get; set; }

    [Column("OldReturnedTime", TypeName = "datetime")]
    public DateTime? OldReturnedTime { get; set; }

    [Column("OldLeftTime", TypeName = "datetime")]
    public DateTime? OldLeftTime { get; set; }

    [Column("DeleteTime", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string DeleteTime { get; set; }

    [Column("DeleteReason", TypeName = "nvarchar(1024)")]
    [StringLength(1024)]
    [MaxLength(1024)]
    public string DeleteReason { get; set; }

    [Column("RequestedDate", TypeName = "datetime")]
    public DateTime? RequestedDate { get; set; }

    public virtual BusinessTime BusinessTime { get; set; }

    public virtual EmployeeCheckInAndOut CheckInOut { get; set; }
}