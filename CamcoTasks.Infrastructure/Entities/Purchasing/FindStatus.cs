// This File Needs to be reviewed Still. Don't Remove this comment.

using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Keyless]
[Table("Purchasing_FindStatus")]
public class FindStatus
{
    [Column("Purchase Request Initiator", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PurchaseRequestInitiator { get; set; }

    [Column("Date Requested:", TypeName = "datetime")]
    public DateTime? DateRequested { get; set; }

    [Column("Item Requested:", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ItemRequested { get; set; }

    [Column("Description:", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Description { get; set; }

    [Column("Status:", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string Status { get; set; }

    [Column("QRN (if known):", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string QRNIfKnown { get; set; }

    [Column("Purchase Order", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string PurchaseOrder { get; set; }

    [Column("Due Date", TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [Column("Receive Info", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ReceiveInfo { get; set; }
}