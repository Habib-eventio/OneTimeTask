// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_EngineeringReviews_NoteItems")]
public class EngineeringReviewNoteItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ItemID", TypeName = "int")]
    public int Id { get; set; }

    [Column("NoteID", TypeName = "int")] public int NoteId { get; set; }

    [Column("OpNumber", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string OperationNumber { get; set; }

    [Column("Remarks", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Remarks { get; set; }

    [Column("FixtureNeeded", TypeName = "bit")]
    public bool FixtureNeeded { get; set; }

    [Column("CmmProgram", TypeName = "bit")]
    public bool CmmProgram { get; set; }

    [Column("Tooling", TypeName = "bit")] public bool IsTooling { get; set; }

    [Column("EngineerNeeded", TypeName = "bit")]
    public bool IsEngineerNeeded { get; set; }

    [Column("Gaging", TypeName = "bit")] public bool IsGaging { get; set; }

    [Column("PersonnelSafety", TypeName = "bit")]
    public bool IsPersonnelSafety { get; set; }

    [Column("ProductSafety", TypeName = "bit")]
    public bool IsProductSafety { get; set; }
}