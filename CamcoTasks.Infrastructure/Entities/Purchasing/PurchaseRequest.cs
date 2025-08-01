// This File Needs to be reviewed Still. Don't Remove this comment.
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Purchasing;

[Table("Purchasing_PurchaseRequests")]
public class PurchaseRequest
{
    [Column("Date Requested:", TypeName = "datetime")]
    public DateTime? DateRequested { get; set; }

    [Column("Initiator", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Initiator { get; set; }

    [Column("SO# (if known):", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public string ShopOrderNumberIfKnown { get; set; }

    [Column("QRN (if known):", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string QRNIfKnown { get; set; }

    [Column("Qty:", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Quantity { get; set; }

    [Column("QuantityReceived", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string QuantityReceived { get; set; }

    [Column("Description:", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Description { get; set; }

    [Column("ASAP (Yes or No)", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ASAPYesOrNo { get; set; }

    [Column("Status:", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Status { get; set; }

    [Column("Item Requested:", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ItemRequested { get; set; }

    [Column("Purchase Order", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string PurchaseOrder { get; set; }

    [Column("Due Date", TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [Column("Receive Info", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ReceiveInfo { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PRNum", TypeName = "int")]
    public int Id { get; set; }

    [Column("Receive Date", TypeName = "datetime")]
    public DateTime? ReceiveDate { get; set; }

    [Column("NOTES", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Notes { get; set; }

    [Column("Vendor", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Vendor { get; set; }

    [Column("PONum", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Ponum { get; set; }

    [Column("ReviewerApproved", TypeName = "bit")]
    public bool ReviewerApproved { get; set; }

    [Column("RecommendedVender", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RecommendedVendor { get; set; }

    [Column("AssignedTo", TypeName = "nvarchar(MAX)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string AssignedTo { get; set; }

    [Column("IsHoldingUpProduction", TypeName = "bit")]
    public bool IsHoldingUpProduction { get; set; }

    [Column("Manufacturer", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Manufacturer { get; set; }

    [Column("ManufacturerPartNumber", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ManufacturerPartNumber { get; set; }

    [Column("Link", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Link { get; set; }

    [Column("UnitOfMeasure", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string UnitOfMeasure { get; set; }

    [Column("RequestorNote", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string RequestorNote { get; set; }

    [Column("NeedsQRN", TypeName = "bit")] public bool NeedsQRN { get; set; }

    [Column("AccountingCode", TypeName = "nvarchar(1)")]
    [StringLength(1)]
    [MaxLength(1)]
    public string AccountingCode { get; set; }

    [Column("PreferredShippingMethod", TypeName = "int")]
    public int? PreferredShippingMethod { get; set; }

    [Column("QuoteLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string QuoteLink { get; set; }

    [Column("SignedQuoteLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string SignedQuoteLink { get; set; }

    [Column("DateClosed", TypeName = "datetime2(7)")]
    public DateTime? DateClosed { get; set; }

    [Browsable(false)]
    [NotMapped]
    [Column("sqlTimestamp", TypeName = "timestamp")]
    public byte[] SqlTimestamp { get; set; }

    [ForeignKey("ShopOrder")]
    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }

    //public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
}