// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_LatheCheckSheet_MasterGageInformation")]
public class LatheCheckSheetMasterGageInformation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("CheckSheetId", TypeName = "int")]
    public int? CheckSheetId { get; set; }

    [Column("Qrn", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string QRN { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Description { get; set; }
}