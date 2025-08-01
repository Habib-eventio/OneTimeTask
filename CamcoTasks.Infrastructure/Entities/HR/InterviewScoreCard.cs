using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_InterviewScoreCards")]
public class InterviewScoreCard
{
    [Key]
    [Column("Id", TypeName = "bigint")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("CandidateFirstName", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string CandidateFirstName { get; set; }

    [Column("CandidateLastName", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string CandidateLastName { get; set; }

    [Column("WonderLicScore", TypeName = "decimal(18, 2)")]
    public decimal WonderLicScore { get; set; }

    [Column("MathTestScore", TypeName = "decimal(18, 2)")]
    public decimal MathTestScore { get; set; }

    [Column("SalaryRequested", TypeName = "decimal(18, 2)")]
    public decimal SalaryRequested { get; set; }

    [Column("InterviewDate", TypeName = "datetime2(7)")]
    public DateTime InterviewDate { get; set; }

    [Browsable(false)]
    [Column("ConductedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectConductedByIdEmployeeId Column). Please refer to CorrectConductedByIdEmployeeId")]
    public long? ObsoleteConductedById { get; set; }

    [Column("CorrectConductedByIdEmployeeId", TypeName = "bigint")]
    public long ConductedByEmployeeId { get; set; }

    [Column("DateApplicantCanStart", TypeName = "datetime")]
    public DateTime DateApplicantCanStart { get; set; }

    [Column("IsCandidateOnTime", TypeName = "bit")]
    public bool IsCandidateOnTime { get; set; }

    [Column("Comments", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string Comments { get; set; }

    [Column("IsQualifiedForThisPosition", TypeName = "bit")]
    public bool IsQualifiedForThisPosition { get; set; }

    [Column("IsQualifiedForAnotherPosition", TypeName = "bit")]
    public bool IsQualifiedForAnotherPosition { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Browsable(false)]
    [Column("UpdatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByIdEmployeeId Column). Please refer to CorrectUpdatedByIdEmployeeId")]
    public long? ObsoleteUpdatedById { get; set; }

    [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
    public long? UpdatedByEmployeeId { get; set; }

    [Column("DateUpdated", TypeName = "datetime2(7)")]
    public DateTime? DateUpdated { get; set; }

}