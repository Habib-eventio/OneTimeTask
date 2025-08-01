// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.TimeClock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TimeClock;

[Table("TimeClock_BusinessTime")]
public class BusinessTime
{
    public BusinessTime()
    {
        TimeClockBusinessTimeRequests = new List<BusinessTimeRequest>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("EmpId", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string EmployeeId { get; set; }

    [Column("LeftTime", TypeName = "datetime")]
    public DateTime? LeftTime { get; set; }

    [Column("ReturnedTime", TypeName = "datetime")]
    public DateTime? ReturnedTime { get; set; }

    [Column("PlaceOfGoing", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PlaceOfGoing { get; set; }

    [Column("CheckInOutId", TypeName = "int")]
    public int? CheckInOutId { get; set; }

    [Column("IsDelete", TypeName = "bit")] public bool? IsDelete { get; set; }

    [Column("IsPunchInOffSite", TypeName = "bit")]
    public bool? IsPunchInOffSite { get; set; }

    [Column("IsPunchOutOffSite", TypeName = "bit")]
    public bool? IsPunchOutOffSite { get; set; }

    public virtual EmployeeCheckInAndOut CheckInOut { get; set; }

    public virtual ICollection<BusinessTimeRequest> TimeClockBusinessTimeRequests { get; set; }
}