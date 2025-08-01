// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_LatheCheckSheetFeatureTypes")]
public class LatheCheckSheetFeatureType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("FeatureType", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string FeatureType { get; set; }

    [Column("IsOneDimensionAllow", TypeName = "bit")]
    public bool? IsOneDimensionAllow { get; set; }
}