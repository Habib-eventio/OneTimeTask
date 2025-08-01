using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{

[Table("UpdateNotes")]
public class UpdateNote
{
    [Column("ID", TypeName = "int")]
    [Key]
    public int Id { get; set; }

    [Column("UpdateId", TypeName = "int")]
    public int? UpdateId { get; set; }

    [Column("NoteDate", TypeName = "datetime")]
    public DateTime NoteDate { get; set; }

    [Column("Notes", TypeName = "nvarchar(MAX)")]
    [Required]
    [MaxLength]
    public string Notes { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    public virtual TaskUpdate Update { get; set; }
}
}
