using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Repository.HR;

[Table("HR_PermissionActions")]
public class PermissionAction
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("PermissionSectionId", TypeName = "int")]
    public int PermissionSectionId { get; set; }

    [Column("ActionName", TypeName = "nvarchar(100)")]
    [Required]
    [MaxLength(50)]
    public string ActionName { get; set; }
}