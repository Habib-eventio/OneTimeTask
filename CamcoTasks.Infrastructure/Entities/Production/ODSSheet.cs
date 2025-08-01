// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_ODSSheets")]
public class ODSSheet
{
    [Column("ODSSheet1", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ODSSheet1 { get; set; }

    [Column("ODSSheet2", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ODSSheet2 { get; set; }

    [Column("ODSSheet3", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ODSSheet3 { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ODSSheetID", TypeName = "int")]
    public int Id { get; set; }
}