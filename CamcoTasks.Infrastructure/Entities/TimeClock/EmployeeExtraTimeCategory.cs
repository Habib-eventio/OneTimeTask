// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TimeClock;

[Table("TimeClock_EmployeeExtraTimeCategories")]
public class EmployeeExtraTimeCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("Field", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Field { get; set; }
}