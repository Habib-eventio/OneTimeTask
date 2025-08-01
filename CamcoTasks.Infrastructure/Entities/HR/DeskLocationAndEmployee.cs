using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_DeskLocationAndEmployees")]
public class DeskLocationAndEmployee
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("EmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("DeskLocationId", TypeName = "bigint")]
    public long DeskLocationId { get; set; }
}