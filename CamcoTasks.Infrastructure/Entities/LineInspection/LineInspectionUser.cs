using CamcoTasks.Infrastructure.Entities.HR;
using ERP.Data.Entities.HR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.LineInspection;

[Table("LineInspection_Users")]
public class LineInspectionUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("UserType", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string UserType { get; set; }

    [Column("UserId", TypeName = "bigint")]
    public long? UserId { get; set; }

    [ForeignKey("UserId")] public virtual Employee Employee { get; set; }
}