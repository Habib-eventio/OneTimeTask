// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_NonConformingParts_Causes")]
public class NonConformingPartCause
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CauseID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Cause", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Cause { get; set; }

    [Column("FourMCategory", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string FourMCategory { get; set; }
}