// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing_MaterialPurchaseOrder")]
public class MaterialPurchaseOrder
{
    [Column("MATERIAL", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Material { get; set; }

    [Column("FIELD 1", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Field1 { get; set; }

    [Column("FIELD 2", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Field2 { get; set; }

    [Column("FIELD 3", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Field3 { get; set; }

    [Column("PRICE PER", TypeName = "money")]
    public decimal? PricePer { get; set; }

    [Column("UOM", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Uom { get; set; }

    [Column("MIN ORDER QTY", TypeName = "float")]
    public double? MinimumOrderQuantity { get; set; }

    [Column("DATA COMBINE", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string DataCombine { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }
}