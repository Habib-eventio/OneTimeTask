// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TimeClock;

[Table("TimeClock_DepartmentAbbreviatedNames")]
public class DepartmentAbbreviatedName
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("DepartmentName", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string DepartmentName { get; set; }

    [Column("DeptAbbreviatedName", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string DeptAbbreviatedName { get; set; }
}