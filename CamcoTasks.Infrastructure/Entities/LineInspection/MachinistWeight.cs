using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.LineInspection;

[Table("LineInspection_MachinistWeight")]
public class MachinistWeight
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("OperatorId", TypeName = "bigint")]
    public long? OperatorId { get; set; }

    [Column("Qa_Eval", TypeName = "int")] public int? QaEval { get; set; }

    [Column("Special", TypeName = "int")] public int? Special { get; set; }

    [Column("Experience", TypeName = "int")]
    public int? Experience { get; set; }

    [Column("Active", TypeName = "bit")] public bool IsActive { get; set; }
}