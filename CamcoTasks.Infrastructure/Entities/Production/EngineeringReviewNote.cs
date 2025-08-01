// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_EngineeringReviews_Notes")]
public class EngineeringReviewNote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("NoteID", TypeName = "int")]
    public int Id { get; set; }

    [Column("ReviewID", TypeName = "int")] public int ReviewId { get; set; }

    [Column("Note", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Note { get; set; }

    [Column("ReviewedBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string ReviewedBy { get; set; }
}