using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.LineInspection;

[Table("LineInspection_PartWeight")]
public class PartWeight
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string PartNumber { get; set; }

    [Column("PartComplexity", TypeName = "int")]
    public int? PartComplexity { get; set; }
}