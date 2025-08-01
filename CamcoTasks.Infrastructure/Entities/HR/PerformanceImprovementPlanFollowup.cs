using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_PerformanceImprovementPlanFollowups")]
public class PerformanceImprovementPlanFollowup
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("DatePipFollowupCreated", TypeName = "datetime2(7)")]
    public DateTime DatePipFollowupCreated { get; set; }

    [Column("MentorResultsAndObservations", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    [Required]
    public string MentorResultsAndObservations { get; set; }

    [Column("EmployeeObservations", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    [Required]
    public string EmployeeObservations { get; set; }

    [Column("IsNextFollowupNeeded", TypeName = "bit")]
    public bool IsNextFollowupNeeded { get; set; }

    [Column("NextFollowUpDate", TypeName = "datetime2(7)")]
    public DateTime? NextFollowUpDate { get; set; }

    [Column("SignedDocument", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string SignedDocument { get; set; }

    [Column("PerformanceImprovementPlanId", TypeName = "bigint")]
    public long PerformanceImprovementPlanId { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long CreatedByEmployeeId { get; set; }

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

    [Column("AdditionalDetails", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(150)]
    [Required]
    public string AdditionalDetails { get; set; }

    [Column("EnumPipFrequencyId", TypeName = "smallint")]
    public short? EnumPipFrequencyId { get; set; }

    public virtual PerformanceImprovementPlan PerformanceImprovementPlan { get; set; }
}