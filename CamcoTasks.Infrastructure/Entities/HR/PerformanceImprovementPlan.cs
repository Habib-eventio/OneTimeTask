using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_PerformanceImprovementPlans")]
public class PerformanceImprovementPlan
{
    public PerformanceImprovementPlan()
    {
        HrPerformanceImprovementPlanFollowups = new HashSet<PerformanceImprovementPlanFollowup>();
    }

    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("EnumPipFrequencyId", TypeName = "smallint")]
    public short EnumPipFrequencyId { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("DatePipCreated", TypeName = "datetime2(7)")]
    public DateTime DatePipCreated { get; set; }

    [Column("NextFollowUpDate", TypeName = "datetime2(7)")]
    public DateTime? NextFollowUpDate { get; set; }

    [Browsable(false)]
    [Column("PipMentorId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectPipMentorIdEmployeeId Column). Please refer to CorrectPipMentorIdEmployeeId")]
    public long? ObsoletePipMentorId { get; set; }

    [Column("CorrectPipMentorIdEmployeeId", TypeName = "bigint")]
    public long PipMentorEmployeeId { get; set; }

    [Column("PipImprovementOpportunity", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    [Required]
    public string PipImprovementOpportunity { get; set; }

    [Column("Details", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    [Required]
    public string Details { get; set; }

    [Column("AdditionalPipDetails", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    [Required]
    public string AdditionalPipDetails { get; set; }

    [Column("SignedDocument", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string SignedDocument { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long CreatedByEmployeeId { get; set; }

    [Column("IsPipStateClosed", TypeName = "bit")]
    public bool IsPipStateClosed { get; set; }

    [Column("IsPipStateClosedDate", TypeName = "datetime2(7)")]
    public DateTime? IsPipStateClosedDate { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Browsable(false)]
    [Column("EmailEmployeesId", TypeName = "varchar(MAX)")]
    [MaxLength]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmailEmployeesId Column). Please refer to CorrectEmailEmployeesId")]
    public string ObsoleteEmailEmployeesId { get; set; }

    [Column("CorrectEmailEmployeesId", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string EmailEmployeesId { get; set; }

    [Column("PipTitle", TypeName = "varchar(100)")]
    [StringLength(100)]
    [MaxLength(100)]
    [Required]
    public string PipTitle { get; set; }

    public virtual ICollection<PerformanceImprovementPlanFollowup> HrPerformanceImprovementPlanFollowups
    {
        get;
        set;
    }
}