// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_ObsoleteSlowMovingInventory")]
public class ObsoleteSlowMovingInventory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("OSMIDate", TypeName = "datetime")]
    public DateTime? OSMIDate { get; set; }

    [Column("PerformedBy", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string PerformedBy { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string PartNumber { get; set; }

    [Column("Customer", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string Customer { get; set; }

    [Column("OSMIQty", TypeName = "int")] public int? OSMIQuantity { get; set; }

    [Column("OSMILocation", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string OSMILocation { get; set; }

    [Column("OSMIQtyNew", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string OSMIQuantityNew { get; set; }

    [Column("OSMILocationNew", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string OSMILocationNew { get; set; }

    [Column("Source", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string Source { get; set; }

    [Column("OSMILocationOld", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string OSMILocationOld { get; set; }

    [Column("BoxNumber", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string BoxNumber { get; set; }

    [Column("BoxQuantity", TypeName = "int")]
    public int? BoxQuantity { get; set; }

    [Column("MoveToOSMIPalletID", TypeName = "int")]
    public int? MoveToOSMIPalletId { get; set; }

    [Column("BoxNumOFQty")]
    [StringLength(255)]
    [MaxLength(255)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string BoxNumberOFQuantity { get; set; }

    [Browsable(false)]
    [NotMapped]
    [Column("ssmstimestamp", TypeName = "timestamp")]
    public byte[] ssmstimestamp { get; set; }
}