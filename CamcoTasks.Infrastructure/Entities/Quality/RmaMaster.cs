// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_RMA_Master")]
public class RmaMaster
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RMAID", TypeName = "int")]
    public int Id { get; set; }

    [Column("RMA Number", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string RmaNumber { get; set; }

    [Column("Customer", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
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

    [Column("UnitCost", TypeName = "money")]
    public decimal UnitCost { get; set; }

    [Column("PotentialTotalCost", TypeName = "money")]
    public decimal PotentialTotalCost { get; set; }

    [Column("RMADate", TypeName = "datetime")]
    public DateTime Rmadate { get; set; }

    [Column("Reason", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Reason { get; set; }

    [Column("PackingListAccounting", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PackingListAccounting { get; set; }

    [Column("DateReceived", TypeName = "datetime")]
    public DateTime? DateReceived { get; set; }

    [Column("QAStatus", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Qastatus { get; set; }

    [Column("FinancialStatus", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string FinancialStatus { get; set; }

    [Column("DateClosed", TypeName = "datetime")]
    public DateTime? DateClosed { get; set; }

    [Column("Comment", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Comment { get; set; }

    [Column("CustomerReferenceNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerReferenceNumber { get; set; }

    [Column("CreditMemo", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CreditMemo { get; set; }

    [Column("DebitCreditMemo", TypeName = "money")]
    public decimal? DebitCreditMemo { get; set; }

    [Column("CparNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CparNumber { get; set; }

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

    [Column("QuantityReceived", TypeName = "float")]
    public double QuantityReceived { get; set; }

    [Column("CreditUsed", TypeName = "money")]
    public decimal CreditUsed { get; set; }

    [Column("PartDisposition", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string PartDisposition { get; set; }

    [Column("RequestedBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string RequestedBy { get; set; }

    [Column("AssignedTo", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string AssignedTo { get; set; }

    [Column("ResponsibleDepartmentId", TypeName = "int")]
    public int? ResponsibleDepartmentId { get; set; }
}