using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_ApplicantStatus")]
public class ApplicantStatus
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("StatusName", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string StatusName { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("ApplicationGroupId", TypeName = "int")]
    public int? ApplicationGroupId { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Browsable(false)]
    [Column("UpdatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByIdEmployeeId Column). Please refer to CorrectUpdatedByIdEmployeeId")]
    public long? ObsoleteUpdatedById { get; set; }

    [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
    public long? UpdatedByEmployeeId { get; set; }

    [Column("DateUpdated", TypeName = "datetime2(7)")]
    public DateTime? DateUpdated { get; set; }

    [Column("IsPhone", TypeName = "bit")] public bool IsPhone { get; set; }

    [Column("IsDateApplied", TypeName = "bit")]
    public bool IsDateApplied { get; set; }

    [Column("IsPhoneInterviewTime", TypeName = "bit")]
    public bool IsPhoneInterviewTime { get; set; }

    [Column("IsPhoneInterviewDate", TypeName = "bit")]
    public bool IsPhoneInterviewDate { get; set; }

    [Column("IsOnSiteInterviewDate", TypeName = "bit")]
    public bool IsOnSiteInterviewDate { get; set; }

    [Column("IsOnSiteInterviewTime", TypeName = "bit")]
    public bool IsOnSiteInterviewTime { get; set; }

    [Column("IsDateHired", TypeName = "bit")]
    public bool IsDateHired { get; set; }

    [Column("IsStartDate", TypeName = "bit")]
    public bool IsStartDate { get; set; }

    [Column("IsOnSiteInterviewer", TypeName = "bit")]
    public bool IsOnSiteInterviewer { get; set; }

    [Column("IsInterviewSet", TypeName = "bit")]
    public bool? IsInterviewSet { get; set; }

    [Column("IsNumberOfContactAttempts", TypeName = "bit")]
    public bool IsNumberOfContactAttempts { get; set; }

    [Column("IsLastContactAttemptDate", TypeName = "bit")]
    public bool IsLastContactAttemptDate { get; set; }

    [Column("IsPositionAppliedFor", TypeName = "bit")]
    public bool IsPositionAppliedFor { get; set; }

    [Column("IsReason", TypeName = "bit")] public bool IsReason { get; set; }

    [Column("IsNotes", TypeName = "bit")] public bool IsNotes { get; set; }

    [Column("IsApplicantName", TypeName = "bit")]
    public bool IsApplicantName { get; set; }

    [Column("IsMethod", TypeName = "bit")] public bool IsMethod { get; set; }

    [Column("IsApplicationAttachment", TypeName = "bit")]
    public bool IsApplicationAttachment { get; set; }

    [Column("IsResumeAttachment", TypeName = "bit")]
    public bool IsResumeAttachment { get; set; }

    [Column("IsOthersAttachment", TypeName = "bit")]
    public bool IsOthersAttachment { get; set; }
}