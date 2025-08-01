using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{

[Table("Tasks_Tasks")]
public class TaskTask
{
    [Key]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("TaskID", TypeName = "int")]
    public int? TaskId { get; set; }

    [Column("TaskType", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? TaskType { get; set; }

    [Column("Priority", TypeName = "int")]
    public int? Priority { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? Description { get; set; }

    [Column("DateAdded", TypeName = "datetime")]
    public DateTime? DateAdded { get; set; }

    [Column("DateCompleted", TypeName = "datetime")]
    public DateTime? DateCompleted { get; set; }

    [Column("Update", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? Update { get; set; }

    [Column("Initiator", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? Initiator { get; set; }

    [Column("Progress", TypeName = "int")]
    public int Progress { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("PictureLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? PictureLink { get; set; }

    [Column("IsReviewed", TypeName = "bit")]
    public bool IsReviewed { get; set; }

    [Column("FileLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? FileLink { get; set; }

    [Column("NudgeCount", TypeName = "int")]
    public int? NudgeCount { get; set; }

    [Column("Department", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? Department { get; set; }

    [Column("ParentTaskId", TypeName = "int")]
    public int? ParentTaskId { get; set; }

    [Column("EmailsList", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? EmailsList { get; set; }

    [Column("EmailCount", TypeName = "int")]
    public int? EmailCount { get; set; }

    [Column("UpcomingDate", TypeName = "datetime")]
    public DateTime? UpcomingDate { get; set; }

    [Column("StartDate", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("PersonResponsible", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? PersonResponsible { get; set; }

    [Column("StatusTypeId", TypeName = "int")]
    public int? StatusTypeId { get; set; }

    [Column("CostingCode", TypeName = "int")]
    public int? CostingCode { get; set; }

    [Column("Summary", TypeName = "nvarchar(2000)")]
    [StringLength(2000)]
    [MaxLength(2000)]
    public string? Summary { get; set; }

    [Column("LastUpdate", TypeName = "datetime")]
    public DateTime? LastUpdate { get; set; } = DateTime.Now;

    [Column("DueDate", TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<TaskUpdate> TasksTaskUpdates { get; set; }

    public TaskTask()
    {
        TasksTaskUpdates = new HashSet<TaskUpdate>();
    }
}
}
