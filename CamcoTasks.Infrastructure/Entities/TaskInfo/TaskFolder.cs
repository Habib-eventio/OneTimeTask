using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{

[Table("Tasks_Folder")]
public class TaskFolder
{
    [Key]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("FolderName", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string FolderName { get; set; }

    [Column("CreatedAt", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<TaskTaskType> TaskTaskTypes { get; set; } = new List<TaskTaskType>();
}
}
