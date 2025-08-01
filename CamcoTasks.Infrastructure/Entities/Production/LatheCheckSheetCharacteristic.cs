// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_LatheCheckSheetCharacteristics")]
public class LatheCheckSheetCharacteristic
{
    [Column("FeautureDescription", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string FeatureDescription { get; set; }

    [Column("MeasurementResult", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string MeasurementResult { get; set; }

    [Column("InspectionMethod", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string InspectionMethod { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("FeatureTypeId", TypeName = "int")]
    public int? FeatureTypeId { get; set; }

    [Column("InspectionFrequency", TypeName = "int")]
    public int? InspectionFrequency { get; set; }

    [Column("BubbleNumber", TypeName = "int")]
    public int? BubbleNumber { get; set; }

    [Column("ActualDimensionFound", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ActualDimensionFound { get; set; }

    [Column("CheckSheetId", TypeName = "int")]
    public int? CheckSheetId { get; set; }

    [Column("SequenceNumber", TypeName = "int")]
    public int? SequenceNumber { get; set; }

    [Column("UpperPrintDimension", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string UpperPrintDimension { get; set; }

    [Column("LowerPrintDimension", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string LowerPrintDimension { get; set; }
}