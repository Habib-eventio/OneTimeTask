// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_RMA_Vendor")]
public class RmaVendor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("VenRMAID", TypeName = "int")]
    public int Id { get; set; }

    [Column("QualityHoldId", TypeName = "int")]
    public int? QualityHoldId { get; set; }

    [Column("ScrapTicketId", TypeName = "int")]
    public int? ScrapTicketId { get; set; }

    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }

    [Column("PurchaseOrderId", TypeName = "int")]
    public int? PurchaseOrderId { get; set; }

    [Column("CoordinatorTaskId", TypeName = "int")]
    public int? CoordinatorTaskId { get; set; }

    [Column("CPARId", TypeName = "int")] public int? CPARId { get; set; }

    [Column("FinanceTaskId", TypeName = "int")]
    public int? FinanceTaskId { get; set; }

    [Column("RMA Number", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RmaNumber { get; set; }

    [Column("Customer", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Customer { get; set; }

    [Column("Champion", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string Champion { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string PartNumber { get; set; }

    [Column("Quantity", TypeName = "int")] public int Quantity { get; set; }

    [Column("QuantityReturned", TypeName = "int")]
    public int QuantityReturned { get; set; }

    [Column("UnitCost", TypeName = "money")]
    public decimal? UnitCost { get; set; }

    [Column("TotalCost", TypeName = "money")]
    public decimal? TotalCost { get; set; }

    [Column("RMADate", TypeName = "datetime")]
    public DateTime RmaDate { get; set; }

    [Column("DateSent", TypeName = "datetime")]
    public DateTime? DateSent { get; set; }

    [Column("Reason", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Reason { get; set; }

    [Column("PackingListAccounting", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PackingListAccounting { get; set; }

    [Column("Status", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Status { get; set; }

    [Column("DateClosed", TypeName = "datetime")]
    public DateTime? DateClosed { get; set; }

    [Column("Comment", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Comment { get; set; }

    [Column("OtherCustomerReference", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string OtherCustomerReference { get; set; }

    [Column("CreditMemo", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CreditMemo { get; set; }

    [Column("DebitCreditMemo", TypeName = "money")]
    public decimal? DebitCreditMemo { get; set; }

    [Column("IsMaterialReturn", TypeName = "bit")]
    public bool? IsMaterialReturn { get; set; }

    [Column("IsEnteredInQuickbooks", TypeName = "bit")]
    public bool? IsEnteredInQuickbooks { get; set; }

    [Column("ShippingMethod", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ShippingMethod { get; set; }

    [Column("TrackingNumber", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string TrackingNumber { get; set; }

    [Column("ShippingAddress", TypeName = "nvarchar(150)")]
    [StringLength(150)]
    [MaxLength(150)]
    public string ShippingAddress { get; set; }

    [Column("ShippingAccountNumber", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string ShippingAccountNumber { get; set; }

    [Column("NeedToBeCamViewed", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string NeedToBeCamViewed { get; set; }

    [Column("CameraViewed", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CameraViewed { get; set; }

    [Column("Viewer", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Viewer { get; set; }

    [Column("ViewResults", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ViewResults { get; set; }

    [Column("EnteredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string EnteredBy { get; set; }

    [Column("DateEntered", TypeName = "datetime2(7)")]
    public DateTime? DateEntered { get; set; }

    [Column("ReturnShippingDate", TypeName = "datetime2(7)")]
    public DateTime? ReturnShippingDate { get; set; }

    [Column("ShopOrderNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ShopOrderNumber { get; set; }

    [Column("PurchaseOrderNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PurchaseOrderNumber { get; set; }
}