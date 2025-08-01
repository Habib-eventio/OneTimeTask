// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_CustomerMaster")]
public class CustomerMaster
{
    [Column("CUST_CODE", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerCode { get; set; }

    [Column("CUST_NAME", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerName { get; set; }

    [Column("CUST_STREET", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerStreet { get; set; }

    [Column("CUST_CITY", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerCity { get; set; }

    [Column("CUST_STATE", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerState { get; set; }

    [Column("CUST_ZIP", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerZip { get; set; }

    [Column("CUST_CONTACT", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerContact { get; set; }

    [Column("CUST_PHONE", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerPhone { get; set; }

    [Column("CUST_FAX", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerFax { get; set; }

    [Column("CUST_E-MAIL", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerEmail { get; set; }

    [Column("CUST_TERMS", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerTerms { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("IsFlowDownRequired", TypeName = "bit")]
    public bool IsFlowDownRequired { get; set; }

    [Column("AverageDaysAfterInvoice", TypeName = "int")]
    public int AverageDaysAfterInvoice { get; set; }

    [Column("FOLDER_NAME", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string FolderName { get; set; }

    [Column("IsActive", TypeName = "bit")] public bool? IsActive { get; set; }

    [Obsolete(
        "CUST_COORDINATOR is deprecated, Please refer to CoordinatorID in Customer_CustomerCoordinator to get CustomerCoordinator related Information")]
    [Column("CUST_COORDINATOR", TypeName = "bigint")]
    public long? CustomerCoordinator { get; set; }

    [Browsable(false)]
    [NotMapped]
    [Column(TypeName = "timestamp")]
    [Timestamp]
    public byte[] SSMA_TimeStamp { get; set; }

    [Column("IsHighValueCustomer", TypeName = "bit")]
    public bool? IsHighValueCustomer { get; set; }

    [Column("CustomerAbbreviationId", TypeName = "int")]
    public int? CustomerAbbreviationId { get; set; }

    [Column("PriceRolledUpNote", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PriceRolledUpNote { get; set; }

    [Column("ShipmentEarlyArrivalDays", TypeName = "int")]
    public int ShipmentEarlyArrivalDays { get; set; }

    [Column("HasSpecialRouting", TypeName = "bit")]
    public bool HasSpecialRouting { get; set; }

    [Column("IsConfidential", TypeName = "bit")]
    public bool IsConfidential { get; set; }
}