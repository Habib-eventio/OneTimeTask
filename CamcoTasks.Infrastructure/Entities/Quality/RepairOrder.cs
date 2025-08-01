// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_RepairOrders")]
public class RepairOrder
{
    [Key]
    [Column("RepairOrderID", TypeName = "int")]
    public int Id { get; set; }

    [Column("DateOpened", TypeName = "datetime2(7)")]
    public DateTime? DateOpened { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PartNumber { get; set; }

    [Column("Customer", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Customer { get; set; }

    [Column("SO#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string So { get; set; }

    [Column("Quantity", TypeName = "int")] public int? Quantity { get; set; }

    [Column("DateDue", TypeName = "datetime2(7)")]
    public DateTime? DateDue { get; set; }

    [Column("DateClosed", TypeName = "datetime2(7)")]
    public DateTime? DateClosed { get; set; }

    [Column("Notes", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Notes { get; set; }

    [Column("RMA#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Rma { get; set; }
}