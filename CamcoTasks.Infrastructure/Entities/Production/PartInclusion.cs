// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Production;

[Table("Production_PartInclusions")]
public class PartInclusion
{
    [Column("ps_par", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PartNumber { get; set; }

    [Column("ps_comp", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ComponentPartNumber { get; set; }

    [Column("ps_qty_per", TypeName = "float")]
    public double? ComponentsNeededPerMainPart { get; set; }

    [Column("CUST_NAME", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string CustomerName { get; set; }

    [Column("sct_mtl_tl", TypeName = "float")]
    public double? sct_mtl_tl { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("InclusionID", TypeName = "int")]
    public int Id { get; set; }
}