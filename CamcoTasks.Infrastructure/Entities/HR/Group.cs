using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_NewGroups")]
public class Group
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("GroupName", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string GroupName { get; set; }
}