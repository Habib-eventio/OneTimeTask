using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.LineInspection;

[Table("LineInspection_Multipliers")]
public class Multiplier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("Field", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Field { get; set; }

    [Column("Multiplier", TypeName = "float")]
    public double? MultiplierValue { get; set; }

    [Column("Table", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Table { get; set; }
}