// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_LatheCheckSheet_Notes")]
public class LatheCheckSheetNote
{
    [Column("Id", TypeName = "int")] public int Id { get; set; }

    [Column("CheckSheetId", TypeName = "int")]
    public int? CheckSheetId { get; set; }

    [Column("NoteType", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string NoteType { get; set; }

    [Column("NoteDescription", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string NoteDescription { get; set; }
}