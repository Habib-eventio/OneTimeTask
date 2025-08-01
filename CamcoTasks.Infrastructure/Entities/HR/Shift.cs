using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_Shifts")]
public class Shift
{
    [Column("Id", TypeName = "smallint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }

    [Column("ShiftName", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string ShiftName { get; set; }
}