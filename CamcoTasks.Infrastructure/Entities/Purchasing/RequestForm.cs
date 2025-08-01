// This File Needs to be reviewed Still. Don't Remove this comment.
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing Request Form")]
public class RequestForm
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PRNum", TypeName = "int")]
    public int PRNum { get; set; }

    [Column("Date Requested:", TypeName = "datetime2(0)")]
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
    public string SOIfKnown { get; set; }

    [Column("QRN (if known):", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string QRNIfKnown { get; set; }

    [Column("Qty:", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Quantity { get; set; }

    [Column("Description:", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
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

    [Column("Due Date", TypeName = "datetime2(0)")]
    public DateTime? DueDate { get; set; }

    [Column("Receive Info", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ReceiveInfo { get; set; }

    [Column("Receive Date", TypeName = "datetime2(0)")]
    public DateTime? ReceiveDate { get; set; }

    [Column("NOTES", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Notes { get; set; }

    [Column("Vendor", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Vendor { get; set; }

    [Column("PONum", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PONum { get; set; }

    [Column("ReviewerApproved", TypeName = "bit")]
    public bool? ReviewerApproved { get; set; }

    [Column("RecommendedVender", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RecommendedVendor { get; set; }

    [Browsable(false)]
    [NotMapped]
    [Column("SSMA_TimeStamp", TypeName = "timestamp")]
    public byte[] SSMA_TimeStamp { get; set; }

    [Column("AccountingCode", TypeName = "char(1)")]
    [StringLength(1)]
    [MaxLength(1)]
    public string AccountingCode { get; set; }

    [ForeignKey("ShopOrder")]
    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }

    //public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
}