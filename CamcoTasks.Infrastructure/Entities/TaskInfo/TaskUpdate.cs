using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{

[Table("Tasks_TaskUpdates")]
public class TaskUpdate
{
    [Key]
    [Column("UpdateID", TypeName = "int")]
    public int UpdateId { get; set; }

    [Column("TaskID", TypeName = "int")]
    public int? TaskId { get; set; }

    [Column("UpdateDate", TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    [Column("Update", TypeName = "nvarchar(MAX)")]
    [Required]
    [MaxLength]
    public string Update { get; set; }

    [Column("PictureLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string PictureLink { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("RecurringID", TypeName = "int")]
    public int? RecurringId { get; set; }

    [Column("FileLink", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string FileLink { get; set; }

    [Column("ISAudit", TypeName = "bit")]
    public bool? IsAudit { get; set; }

    [Column("IsPass", TypeName = "bit")]
    public bool? IsPass { get; set; }

    [Column("GraphNumber", TypeName = "decimal(18, 2)")]
    public decimal GraphNumber { get; set; }

    [Column("DueDate", TypeName = "date")]
    public DateTime? DueDate { get; set; }

    [Column("TaskCompleted", TypeName = "bit")]
    public bool? TaskCompleted { get; set; }

    [Column("QuestionAnswer", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string QuestionAnswer { get; set; }

    [Column("FailReason", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string FailReason { get; set; }

    [Column("FailedAuditList", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string FailedAuditList { get; set; }

    [Column("EmailId", TypeName = "int")]
    public int? EmailId { get; set; }

    [Column("IsPicture", TypeName = "bit")]
    public bool? IsPicture { get; set; }

    [Column("CreatedDate", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("PostponeDays", TypeName = "int")]
    public int? PostponeDays { get; set; }

    [Column("PostponeReason", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string PostponeReason { get; set; }

    [Column("UpdateBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string UpdateBy { get; set; }

    [Column("UpdatedDocumentLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string UpdatedDocumentLink { get; set; }

    public virtual RecurringTask Recurring { get; set; }

    public virtual TaskTask Task { get; set; }

    public virtual ICollection<UpdateNote> UpdateNotes { get; set; }

    public TaskUpdate()
    {
        UpdateNotes = new HashSet<UpdateNote>();
    }
}
}
