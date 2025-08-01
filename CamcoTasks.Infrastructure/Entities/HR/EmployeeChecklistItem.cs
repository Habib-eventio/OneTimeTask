using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_EmployeeChecklistItems")]
public class EmployeeChecklistItem
{
    [Key]
    [Column("Id", TypeName = "smallint")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }

    [Column("ItemName", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string ItemName { get; set; }

    [Column("ReposonsiblePersonId", TypeName = "smallint")]
    public short ResponsiblePersonId { get; set; }
}