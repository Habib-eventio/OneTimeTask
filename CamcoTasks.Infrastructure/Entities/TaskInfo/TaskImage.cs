using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{

[Table("TasksImages")]
public class TaskImage
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("RecurringId", TypeName = "int")]
    public int? RecurringId { get; set; }

    [Column("PictureLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string PictureLink { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool? IsDeleted { get; set; }

    [Column("OneTimeId", TypeName = "int")]
    public int? OneTimeId { get; set; }

    [Column("ImageNote", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ImageNote { get; set; }

    [Column("UpdateId", TypeName = "int")]
    public int? UpdateId { get; set; }

    [Column("FileName", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string FileName { get; set; }
}
}
