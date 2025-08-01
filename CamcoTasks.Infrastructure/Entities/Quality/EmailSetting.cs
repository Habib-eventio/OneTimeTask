// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_EmailSettings")]
public class EmailSetting
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("AuditorId", TypeName = "nvarchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    public string AuditorId { get; set; }

    [Column("ManagerId", TypeName = "nvarchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    public string ManagerId { get; set; }

    [Column("TypeId", TypeName = "int")] public int TypeId { get; set; }
}