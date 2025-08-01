// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_Parts_SecondaryInfo")]
public class PartSecondaryInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("PM_PART", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string PmPart { get; set; }

    [Column("PM_WEIGHT", TypeName = "float")]
    public double PmWeight { get; set; }

    [Column("ImageLoc", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ImageLoc { get; set; }
}