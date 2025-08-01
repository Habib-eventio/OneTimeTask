// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_LatheCheckSheet_PdfFiles")]
public class LatheCheckSheetPDFFile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("CheckSheetId", TypeName = "int")]
    public int? CheckSheetId { get; set; }

    [Column("FilePath", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string FilePath { get; set; }
}