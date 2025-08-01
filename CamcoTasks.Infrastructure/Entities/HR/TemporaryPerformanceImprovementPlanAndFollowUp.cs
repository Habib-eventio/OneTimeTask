using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_TemporaryPerformanceImprovementPlanAndFollowUps")]
public class TemporaryPerformanceImprovementPlanAndFollowUp
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

    [Column("MentorResultsAndPipOpportunity", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    public string MentorResultsAndPipOpportunity { get; set; }

    [Column("DetailsAndEmployeeObservations", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    public string DetailsAndEmployeeObservations { get; set; }

    [Column("AdditionalDetails", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    public string AdditionalDetails { get; set; }

    [Browsable(false)]
    [Column("CurrentUserId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCurrentUserIdEmployeeId Column). Please refer to CorrectCurrentUserIdEmployeeId")]
    public long? ObsoleteCurrentUserId { get; set; }

    [Column("CorrectCurrentUserIdEmployeeId", TypeName = "bigint")]
    public long CurrentUserEmployeeId { get; set; }

    [Column("PerformanceImprovementPlanId", TypeName = "bigint")]
    public long? PerformanceImprovementPlanId { get; set; }

    [Column("EnumPipFrequencyId", TypeName = "smallint")]
    public short? EnumPipFrequencyId { get; set; }

    [Column("NextFollowUpDate", TypeName = "datetime2(7)")]
    public DateTime? NextFollowUpDate { get; set; }

    [Browsable(false)]
    [Column("PipMentorId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectPipMentorIdEmployeeId Column). Please refer to CorrectPipMentorIdEmployeeId")]
    public long? ObsoletePipMentorId { get; set; }

    [Column("CorrectPipMentorIdEmployeeId", TypeName = "bigint")]
    public long? PipMentorEmployeeId { get; set; }

    [Column("PipTitle", TypeName = "nchar(10)")]
    [StringLength(10)]
    [MaxLength(10)]
    public string PipTitle { get; set; }

    [Column("FormTypeId", TypeName = "smallint")]
    public short FormTypeId { get; set; }

    [Column("IsNextFollowupNeeded", TypeName = "bit")]
    public bool? IsNextFollowupNeeded { get; set; }

    [Browsable(false)]
    [Column("EmailEmployeesId", TypeName = "varchar(MAX)")]
    [MaxLength]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmailEmployeesId Column). Please refer to CorrectEmailEmployeesId")]
    public string ObsoleteEmailEmployeesId { get; set; }

    [Column("CorrectEmailEmployeesId", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string EmailEmployeesId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }
}