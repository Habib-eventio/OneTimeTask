// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing_ReoccuringExpenses")]
public class RecurringExpense
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("PODate", TypeName = "datetime2(7)")]
    public DateTime PurchaseOrderNumberDate { get; set; }

    [Column("Vendor", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Vendor { get; set; }

    [Column("DueDate", TypeName = "datetime2(7)")]
    public DateTime? DueDate { get; set; }

    [Column("AccountingCode", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string AccountingCode { get; set; }

    [Column("QRN", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string QRN { get; set; }

    [Column("Quantity", TypeName = "int")] public int? Quantity { get; set; }

    [Column("UnitCost", TypeName = "money")]
    public decimal? UnitCost { get; set; }

    [Column("TotalCost", TypeName = "money")]
    public decimal TotalCost { get; set; }

    [Column("ProductOrService", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ProductOrService { get; set; }

    [Column("Initiator", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Initiator { get; set; }

    [Column("Notes", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Notes { get; set; }

    [Column("PONumber", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string PurchaseOrderNumber { get; set; }

    [Column("ExpenseCategory", TypeName = "int")]
    public int? ExpenseCategory { get; set; }

    [Column("Status", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string Status { get; set; }

    [Column("DeletedFlag", TypeName = "bit")]
    public bool? DeletedFlag { get; set; }
}