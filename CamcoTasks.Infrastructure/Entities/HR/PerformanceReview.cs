using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_PerformanceReviews")]
public class PerformanceReview
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("FrequencyId", TypeName = "bigint")]
    public long FrequencyId { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("Date", TypeName = "datetime2(7)")]
    public DateTime Date { get; set; }

    [Column("NextFollowUpDate", TypeName = "datetime2(7)")]
    public DateTime NextFollowUpDate { get; set; }

    [Browsable(false)]
    [Column("ReviewerId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectReviewerIdEmployeeId Column). Please refer to CorrectReviewerIdEmployeeId")]
    public long? ObsoleteReviewerId { get; set; }

    [Column("CorrectReviewerIdEmployeeId", TypeName = "bigint")]
    public long ReviewerEmployeeId { get; set; }

    [Column("ReviewerNotes", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    public string ReviewerNotes { get; set; }

    [Column("DisciplinesId", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string DisciplinesId { get; set; }

    [Column("PiPsId", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string PiPsId { get; set; }

    [Column("AboveAndBeyondRecognitionsId", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string AboveAndBeyondRecognitionsId { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Browsable(false)]
    [Column("EmailEmployeesId", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmailEmployeesId Column). Please refer to CorrectEmailEmployeesId")]
    public string ObsoleteEmailEmployeesId { get; set; }

    [Column("CorrectEmailEmployeesId", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string EmailEmployeesId { get; set; }

    [Column("SignedDocument", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string SignedDocument { get; set; }
}