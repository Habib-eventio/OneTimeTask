using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{
[Table("Tasks_Editor")]
public class TaskEditor
{
    [Key]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Content { get; set; }

    [Column("CreatedDate", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [ForeignKey("ID")]
    public virtual ICollection<TaskFolder> FolderId { get; set; }
}
}
