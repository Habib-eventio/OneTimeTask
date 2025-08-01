// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using ERP.Data.Entities.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TimeClock;

[Table("TimeClock_EmployeeCheckInAndOut")]
public class EmployeeCheckInAndOut
{
    public EmployeeCheckInAndOut()
    {
        TimeClockBusinessTripRequests = new List<BusinessTimeRequest>();
        TimeClockBusinessTrips = new List<BusinessTime>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CheckInOutId", TypeName = "int")]
    public int Id { get; set; }

    [Column("EmpId", TypeName = "nvarchar(20)")]
    [StringLength(20)]
    [MaxLength(20)]
    public string EmployeeId { get; set; }

    [Column("CheckInTime", TypeName = "datetime")]
    public DateTime? CheckInTime { get; set; }

    [Column("CheckOutTime", TypeName = "datetime")]
    public DateTime? CheckOutTime { get; set; }

    [Column("CheckInImage", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CheckInImage { get; set; }

    [Column("CheckOutImage", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CheckOutImage { get; set; }

    [Column("CheckInMacAddress", TypeName = "nvarchar(100)")]
    [StringLength(100)]
    [MaxLength(100)]
    public string CheckInMacAddress { get; set; }

    [Column("CheckOutMacAddress", TypeName = "nvarchar(100)")]
    [StringLength(100)]
    [MaxLength(100)]
    public string CheckOutMacAddress { get; set; }

    [Column("IsRemotelyCheckIn", TypeName = "bit")]
    public bool? IsRemotelyCheckIn { get; set; }

    [Column("IsRemotelyCheckOut", TypeName = "bit")]
    public bool? IsRemotelyCheckOut { get; set; }

    [Column("IsUpworkCheckIn", TypeName = "bit")]
    public bool? IsUpworkCheckIn { get; set; }

    [Column("IsUpworkCheckOut", TypeName = "bit")]
    public bool? IsUpworkCheckOut { get; set; }

    [Column("DateCreated", TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [Column("CreatedById", TypeName = "bigint")]
    public long? CreatedById { get; set; }

    [Column("IsAutomaticCheckIn", TypeName = "bit")]
    public bool IsAutomaticCheckIn { get; set; } = false;

    [Column("IsAutomaticCheckOut", TypeName = "bit")]
    public bool IsAutomaticCheckOut { get; set; } = false;

    public virtual Employee CreatedBy { get; set; }

    public virtual ICollection<BusinessTimeRequest> TimeClockBusinessTripRequests { get; set; }

    public virtual ICollection<BusinessTime> TimeClockBusinessTrips { get; set; }
}