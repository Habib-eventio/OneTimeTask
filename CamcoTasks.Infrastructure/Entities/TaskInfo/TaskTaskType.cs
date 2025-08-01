using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{

[Table("Tasks_Tasks_TaskType")]
public class TaskTaskType
{
    [Key]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("TaskType", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? TaskType { get; set; }

    [Column("Email", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? Email { get; set; }

    [Column("Email2", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? Email2 { get; set; }

    [Column("CreatedAt", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public int? TaskFolderId { get; set; }

    [ForeignKey(nameof(TaskFolderId))]
    public virtual TaskFolder? TaskFolder { get; set; }
}
}

