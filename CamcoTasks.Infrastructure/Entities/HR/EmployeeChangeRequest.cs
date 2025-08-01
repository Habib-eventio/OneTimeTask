using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_EmployeeChangeRequests")]
public class EmployeeChangeRequest
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Browsable(false)]
    [Column("RequesterId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectRequesterIdEmployeeId Column). Please refer to CorrectRequesterIdEmployeeId")]
    public long? ObsoleteRequesterId { get; set; }

    [Column("CorrectRequesterIdEmployeeId", TypeName = "bigint")]
    public long RequesterEmployeeId { get; set; }

    [Browsable(false)]
    [Column("EmployeeToReceiveChangeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeToReceiveChangeIdEmployeeId Column). Please refer to CorrectEmployeeToReceiveChangeIdEmployeeId")]
    public long? ObsoleteEmployeeToReceiveChangeId { get; set; }

    [Column("CorrectEmployeeToReceiveChangeIdEmployeeId", TypeName = "bigint")]
    public long EmployeeToReceiveChangeEmployeeId { get; set; }

    [Column("DateEffective", TypeName = "datetime2(7)")]
    public DateTime DateEffective { get; set; }

    [Column("DateOfRequestExecution", TypeName = "datetime2(7)")]
    public DateTime DateOfRequestExecution { get; set; }

    [Column("Description", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    [Required]
    public string Description { get; set; }

    [Column("IsInternational", TypeName = "bit")]
    public bool IsInternational { get; set; }

    [Column("AnnualSalaryOld", TypeName = "decimal(7,2)")]
    public decimal? AnnualSalaryOld { get; set; }

    [Column("BaseHourlyRateOld", TypeName = "decimal(7,2)")]
    public decimal? BaseHourlyRateOld { get; set; }

    [Column("IsPaymentTypeSalaryOld", TypeName = "bit")]
    public bool IsPaymentTypeSalaryOld { get; set; }

    [Column("TierLevelIdOld", TypeName = "bigint")]
    public long? TierLevelIdOld { get; set; }

    [Column("JobTitleIdOld", TypeName = "bigint")]
    public long? JobTitleIdOld { get; set; }

    [Column("SecondaryJobTitleIdsOld", TypeName = "varchar(100)")]
    [StringLength(100)]
    [MaxLength(100)]
    public string SecondaryJobTitleIdsOld { get; set; }

    [Column("DepartmentIdOld", TypeName = "bigint")]
    public long? DepartmentIdOld { get; set; }

    [Column("ShiftStartTimeOld", TypeName = "datetime2(7)")]
    public DateTime? ShiftStartTimeOld { get; set; }

    [Column("ShiftEndTimeOld", TypeName = "datetime2(7)")]
    public DateTime? ShiftEndTimeOld { get; set; }

    [Column("ShiftDifferentialOld", TypeName = "decimal(7,2)")]
    public decimal? ShiftDifferentialOld { get; set; }

    [Browsable(false)]
    [Column("ManagerIdOld", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectManagerIdOldEmployeeId Column). Please refer to CorrectManagerIdOldEmployeeId")]
    public long? ObsoleteManagerIdOld { get; set; }

    [Column("CorrectManagerIdOldEmployeeId", TypeName = "bigint")]
    public long? ManagerOldEmployeeId { get; set; }

    [Browsable(false)]
    [Column("LeaderIdOld", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectLeaderIdOldEmployeeId Column). Please refer to CorrectLeaderIdOldEmployeeId")]
    public long? ObsoleteLeaderIdOld { get; set; }

    [Column("CorrectLeaderIdOldEmployeeId", TypeName = "bigint")]
    public long? LeaderOldEmployeeId { get; set; }

    [Column("JobStatusIdOld", TypeName = "smallint")]
    public short? JobStatusIdOld { get; set; }

    [Column("ExpectedMinimumHoursOld", TypeName = "decimal(7,2)")]
    public decimal? ExpectedMinimumHoursOld { get; set; }

    [Column("PercentageOfDirectTimeOld", TypeName = "decimal(7,2)")]
    public decimal? PercentageOfDirectTimeOld { get; set; }

    [Column("AnnualSalaryNew", TypeName = "decimal(7,2)")]
    public decimal? AnnualSalaryNew { get; set; }

    [Column("BaseHourlyRateNew", TypeName = "decimal(7,2)")]
    public decimal? BaseHourlyRateNew { get; set; }

    [Column("IsPaymentTypeSalaryNew", TypeName = "bit")]
    public bool IsPaymentTypeSalaryNew { get; set; }

    [Column("TierLevelIdNew", TypeName = "bigint")]
    public long? TierLevelIdNew { get; set; }

    [Column("JobTitleIdNew", TypeName = "bigint")]
    public long? JobTitleIdNew { get; set; }

    [Column("SecondaryJobTitleIdsNew", TypeName = "varchar(100)")]
    [StringLength(100)]
    [MaxLength(100)]
    public string SecondaryJobTitleIdsNew { get; set; }

    [Column("DepartmentIdNew", TypeName = "bigint")]
    public long? DepartmentIdNew { get; set; }

    [Column("ShiftStartTimeNew", TypeName = "datetime2(7)")]
    public DateTime? ShiftStartTimeNew { get; set; }

    [Column("ShiftEndTimeNew", TypeName = "datetime2(7)")]
    public DateTime? ShiftEndTimeNew { get; set; }

    [Column("ShiftDifferentialNew", TypeName = "decimal(7,2)")]
    public decimal? ShiftDifferentialNew { get; set; }

    [Browsable(false)]
    [Column("ManagerIdNew", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectManagerIdNewEmployeeId Column). Please refer to CorrectManagerIdNewEmployeeId")]
    public long? ObsoleteManagerIdNew { get; set; }

    [Column("CorrectManagerIdNewEmployeeId", TypeName = "bigint")]
    public long? ManagerNewEmployeeId { get; set; }

    [Browsable(false)]
    [Column("LeaderIdNew", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectLeaderIdNewEmployeeId Column). Please refer to CorrectLeaderIdNewEmployeeId")]
    public long? ObsoleteLeaderIdNew { get; set; }

    [Column("CorrectLeaderIdNewEmployeeId", TypeName = "bigint")]
    public long? LeaderNewEmployeeId { get; set; }

    [Column("JobStatusIdNew", TypeName = "smallint")]
    public short? JobStatusIdNew { get; set; }

    [Column("ExpectedMinimumHoursNew", TypeName = "decimal(7,2)")]
    public decimal? ExpectedMinimumHoursNew { get; set; }

    [Column("PercentageOfDirectTimeNew", TypeName = "decimal(7,2)")]
    public decimal? PercentageOfDirectTimeNew { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Column("ChangeRequestStatusId", TypeName = "smallint")]
    public short ChangeRequestStatusId { get; set; }

    [Column("IsRequestCompleted", TypeName = "bit")]
    public bool IsRequestCompleted { get; set; }

    [Column("ChangeRequestAttachment", TypeName = "varchar(200)")]
    [StringLength(200)]
    [MaxLength(200)]
    public string ChangeRequestAttachment { get; set; }
}