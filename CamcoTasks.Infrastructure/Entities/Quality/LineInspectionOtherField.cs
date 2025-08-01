// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_LineInspectionOtherFields")]
public class LineInspectionOtherField
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("InspectionAuditId", TypeName = "int")]
    public int? InspectionAuditId { get; set; }

    [Column("SevereRank", TypeName = "int")]
    public int? SevereRank { get; set; }

    [Column("CharacteristicNum", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CharacteristicNum { get; set; }

    [Column("CharacteristicDesc", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CharacteristicDescription { get; set; }

    [Column("MeasurmentOfResults", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string MeasurementOfResults { get; set; }

    [Column("ActionsTakenOrRecommendations", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ActionsTakenOrRecommendations { get; set; }
}