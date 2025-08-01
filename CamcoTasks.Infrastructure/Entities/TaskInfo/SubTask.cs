using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{
[Table("Task_SubTasks")]
public class SubTask
{
    [Key]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("SubTaskDescription", TypeName = "varchar(255)")]
    public string SubTaskDescription { get; set; }

    [Column("TaskId", TypeName = "int")]
    public int TaskId { get; set; }

    [Column("Summary", TypeName = "varchar(500)")]
    public string Summary { get; set; }

    [Column("LastUpdated", TypeName = "datetime")]
    public DateTime? LastUpdated { get; set; }

    [Column("StatusTypeId", TypeName = "int")]
    public int StatusTypeId { get; set; }

    [ForeignKey("TaskId")]
    public virtual TaskTask Task { get; set; }
}
}

