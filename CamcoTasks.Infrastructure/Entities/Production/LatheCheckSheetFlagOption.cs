// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_LatheCheckSheet_FlagOptions")]
public class LatheCheckSheetFlagOption
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("FlagOption", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string FlagOption { get; set; }

    [Column("Active", TypeName = "bit")] public bool? IsActive { get; set; }
}