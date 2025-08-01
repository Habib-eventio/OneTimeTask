using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.TaskInfo
{
[Table("Tasks_FreqList")]
public class FrequencyList
{
    [Column("Frequency", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? Frequency { get; set; }

    [Column("Days", TypeName = "int")]
    public int Days { get; set; }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }
}
}
