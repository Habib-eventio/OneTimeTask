using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_DisciplineAttachments")]
public class DisciplineAttachment
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("DisciplineId", TypeName = "bigint")]
    public long DisciplineId { get; set; }

    [Column("FileName", TypeName = "varchar(200)")]
    [StringLength(200)]
    [MaxLength(200)]
    [Required]
    public string FileName { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }
}