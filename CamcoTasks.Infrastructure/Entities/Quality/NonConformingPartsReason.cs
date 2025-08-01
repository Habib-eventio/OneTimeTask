// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_NonConformingParts_Reasons")]
public class NonConformingPartsReason
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ReasonID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Reason", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Reason { get; set; }
}