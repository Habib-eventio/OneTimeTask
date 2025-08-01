// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_SteelUsage")]
public class SteelUsage
{
    [Column("PART NUMBER", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PartNumber { get; set; }

    [Column("PS_COMP", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PsComp { get; set; }

    [Column("PS_QTY PER", TypeName = "int")]
    public int? PsQtyPer { get; set; }

    [Column("MAT'L TYPE", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string MatLType { get; set; }

    [Column("BrukerMaterialCode", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string BrukerMaterialCode { get; set; }

    [Column("BrukerExpectedReading", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string BrukerExpectedReading { get; set; }

    [Column("BrukerActualReading", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string BrukerActualReading { get; set; }

    [Column("Margin", TypeName = "float")] public double? Margin { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }
}