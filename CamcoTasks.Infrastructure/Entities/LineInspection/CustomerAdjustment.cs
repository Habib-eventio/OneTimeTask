using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.LineInspection;

[Table("LineInspection_CustomerAdjustment")]
public class CustomerAdjustment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("Customer", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string Customer { get; set; }

    [Column("QualReq", TypeName = "int")] public int? QualReq { get; set; }

    [Column("QualIssues", TypeName = "int")]
    public int? QualIssues { get; set; }

    [Column("SpecInf", TypeName = "int")] public int? SpecInf { get; set; }

    [Column("Active", TypeName = "bit")] public bool Active { get; set; }

    [Column("New", TypeName = "int")] public int? New { get; set; }
}