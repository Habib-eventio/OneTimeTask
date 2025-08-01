// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_CusotmerMaterialCodes")]
public class CustomerMaterialCode
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("MaterialCode", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string MaterialCode { get; set; }

    [Column("GradeId", TypeName = "int")] public int GradeId { get; set; }

    [Column("ConditionId", TypeName = "int")]
    public int ConditionId { get; set; }

    [Column("RevisionLevel", TypeName = "nchar(10)")]
    [StringLength(10)]
    [MaxLength(10)]
    [Required]
    public string RevisionLevel { get; set; }

    [Column("UintOfMeasureId", TypeName = "int")]
    public int? UintOfMeasureId { get; set; }

    [Column("CostPerUnitOfMeasure", TypeName = "decimal(18, 4)")]
    public decimal? CostPerUnitOfMeasure { get; set; }

    [Column("DescriptionNote", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string DescriptionNote { get; set; }

    [Column("ExtendedDescription", TypeName = "nvarchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string ExtendedDescription { get; set; }
}