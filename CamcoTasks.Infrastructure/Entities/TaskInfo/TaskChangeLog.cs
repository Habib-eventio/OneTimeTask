using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{

[Table("Tasks_ChangeLogs")]
public class TaskChangeLog
{
    [Key]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("TaskId", TypeName = "int")]
    public int TaskId { get; set; }

    [Column("UserId", TypeName = "int")]
    public long UserId { get; set; }

    [Column("Action", TypeName = "nvarchar(MAX)")]
    public string Action { get; set; }

    [Column("DateModified", TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    [Column("IPAddress", TypeName = "nvarchar(255)")]
    public string IPAddress { get; set; }

    [ForeignKey("TaskId")]
    public virtual TaskTask TaskTask { get; set; }
}
}
