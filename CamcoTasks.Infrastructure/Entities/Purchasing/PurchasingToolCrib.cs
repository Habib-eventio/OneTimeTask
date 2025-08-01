// This File Needs to be reviewed Still. Don't Remove this comment.

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Keyless]
[Table("Purchasing_ToolCrib")]
public class PurchasingToolCrib
{
    [Column("QRN#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string QRN { get; set; }

    [Column("Cell", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Cell { get; set; }

    [Column("Location 1", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Location1 { get; set; }

    [Column("Location 2", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Location2 { get; set; }

    [Column("Location 3", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Location3 { get; set; }

    [Column("Field6", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Field6 { get; set; }

    [Column("Tool", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Tool { get; set; }

    [Column("Type", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Type { get; set; }

    [Column("Description", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Description { get; set; }

    [Column("Mfg Part # /EDP Number", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string MfgPartEDPNumber { get; set; }

    [Column("Inventory", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Inventory { get; set; }
}