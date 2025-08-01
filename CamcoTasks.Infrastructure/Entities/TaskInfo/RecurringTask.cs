using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{

[Table("Tasks_RecTasks")]
public class RecurringTask
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("TaskID", TypeName = "int")]
    public int? TaskId { get; set; }

    [Column("PersonResponsible", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? PersonResponsible { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? Description { get; set; }

    [Column("DateCreated", TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [Column("DateCompleted", TypeName = "datetime")]
    public DateTime? DateCompleted { get; set; }

    [Column("UpcomingDate", TypeName = "datetime")]
    public DateTime? UpcomingDate { get; set; }

    [Column("Updates", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? Updates { get; set; }

    [Column("Initiator", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? Initiator { get; set; }

    [Column("PictureLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? PictureLink { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("IsDeactivated", TypeName = "bit")]
    public bool IsDeactivated { get; set; }

    [Column("IsApproved", TypeName = "bit")]
    public bool IsApproved { get; set; }

    [Column("ApprovedByEmployeeId", TypeName = "bigint")]
    public long? ApprovedByEmployeeId { get; set; }

    [Column("DateApproved", TypeName = "datetime")]
    public DateTime? DateApproved { get; set; }

    [Column("InstructionFileLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? InstructionFileLink { get; set; }

    [Column("NudgeCount", TypeName = "int")]
    public int NudgeCount { get; set; }

    [Column("EmailsList", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? EmailsList { get; set; }

    [Column("CompletedOnTime", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? CompletedOnTime { get; set; }

    [Column("IsGraphRequired", TypeName = "bit")]
    public bool? IsGraphRequired { get; set; }

    [Column("AuditPerson", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? AuditPerson { get; set; }

    [Column("GraphTitle", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? GraphTitle { get; set; }

    [Column("VerticalAxisTitle", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? VerticalAxisTitle { get; set; }

    [Column("IsTrendLine", TypeName = "bit")]
    public bool? IsTrendLine { get; set; }

    [Column("ExternalAuditor", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? ExternalAuditor { get; set; }

    [Column("IsPassOrFail", TypeName = "bit")]
    public bool? IsPassOrFail { get; set; }

    [Column("Question", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? Question { get; set; }

    [Column("IsQuestionRequired", TypeName = "bit")]
    public bool? IsQuestionRequired { get; set; }

    [Column("IsPicRequired", TypeName = "bit")]
    public bool? IsPicRequired { get; set; }

    [Column("UpdateImageType", TypeName = "int")]
    public int? UpdateImageType { get; set; }

    [Column("UpdateImageLoction", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? UpdateImageLocation { get; set; }

    [Column("UpdateImageDescription", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? UpdateImageDescription { get; set; }

    [Column("FailedEmailsList", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? FailedEmailsList { get; set; }

    [Column("IsAuditRequired", TypeName = "bit")]
    public bool? IsAuditRequired { get; set; }

    [Column("LatestGraphValue", TypeName = "decimal(18, 2)")]
    public decimal? LatestGraphValue { get; set; }

    [Column("ParentTaskId", TypeName = "int")]
    public int? ParentTaskId { get; set; }

    [Column("IsDescriptionMandatory", TypeName = "bit")]
    public bool? IsDescriptionMandatory { get; set; } = false;

    [Column("StartDate", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("IsMaxValueRequired", TypeName = "bit")]
    public bool? IsMaxValueRequired { get; set; } = false;

    [Column("MaxYAxisValue", TypeName = "int")]
    public int? MaxYAxisValue { get; set; }

    [Column("IsHandDeliveredRequired", TypeName = "bit")]
    public bool? IsHandDeliveredRequired { get; set; } = false;

    [Column("IsProtected", TypeName = "bit")]
    public bool? IsProtected { get; set; } = false;

    [Column("DueDateReminder", TypeName = "bit")]
    public bool? DueDateReminder { get; set; } = false;

    [Column("EmailCount", TypeName = "int")]
    public int? EmailCount { get; set; }

    [Column("IsPicture", TypeName = "bit")]
    public bool? IsPicture { get; set; } = false;

    [Column("TaskArea", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? TaskArea { get; set; }

    [Column("AuthorizationList", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? AuthorizationList { get; set; }

    [Column("FrequencyId", TypeName = "int")]
    public int FrequencyId { get; set; }

    [Column("TaskDescriptionSubject", TypeName = "nvarchar(60)")]
    [StringLength(60)]
    [MaxLength(60)]
    public string? TaskDescriptionSubject { get; set; }

    [Column("IsRecurring", TypeName = "bit")]
    public bool IsRecurring { get; set; }

    [Column("IsPositionSpecific", TypeName = "bit")]
    public bool IsPositionSpecific { get; set; } = false;

    [Column("JobTitle", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? JobTitle { get; set; }

    [Column("StartDueDateDay", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string? StartDueDateDay { get; set; }

    [Column("EndDueDateDay", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string? EndDueDateDay { get; set; }

    [Column("ExpectMinutes", TypeName = "int")]
    public int? ExpectMinutes { get; set; }

    [Column("Location", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? Location { get; set; }

    [Column("EmailsListJobId", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? EmailsListJobId { get; set; }

    [Column("IsXAxisInterval", TypeName = "bit")]
    public bool? IsXAxisInterval { get; set; } = false;

    [Column("XAxisIntervalTypeId", TypeName = "int")]
    public int? XAxisIntervalTypeId { get; set; }

    [Column("XAxisIntervalRange", TypeName = "float")]
    public double? XAxisIntervalRange { get; set; }

    [Column("TaskStartDueDate", TypeName = "datetime")]
    public DateTime? TaskStartDueDate { get; set; }

    [Column("TaskEndDueDate", TypeName = "datetime")]
    public DateTime? TaskEndDueDate { get; set; }

    [Column("IsTaskDuePeriod", TypeName = "bit")]
    public bool IsTaskDuePeriod { get; set; } = false;

    [Column("IsTaskDelayed", TypeName = "bit")]
    public bool IsTaskDelayed { get; set; } = false;

    [Column("DelayReason", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? DelayReason { get; set; }

    [Column("TaskDelayedDate", TypeName = "datetime")]
    public DateTime? TaskDelayedDate { get; set; }

    [Column("IsTaskRandomize", TypeName = "bit")]
    public bool IsTaskRandomize { get; set; } = false;

    [Column("IsDocumentRequired", TypeName = "bit")]
    public bool IsDocumentRequired { get; set; } = false;

    [Column("UpdatedDocumentLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? UpdatedDocumentLink { get; set; }

    [Column("UpdatedDocumentDescription", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? UpdatedDocumentDescription { get; set; }

    [Column("HandDocumentDeliverTo", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? HandDocumentDeliverTo { get; set; }

    [Column("TaskDeactivatedDate", TypeName = "datetime")]
    public DateTime? TaskDeactivatedDate { get; set; }

    [ForeignKey("FrequencyId")]
    public virtual FrequencyList TasksFrequency { get; set; }

    [JsonIgnore]
    public virtual ICollection<TaskUpdate> TasksTaskUpdates { get; set; }

    public RecurringTask()
    {
        TasksTaskUpdates = new HashSet<TaskUpdate>();
    }
}
}
