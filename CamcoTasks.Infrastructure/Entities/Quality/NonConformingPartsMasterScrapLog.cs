//// This File Needs to be reviewed Still. Don't Remove this comment.
//using CamcoTasks.Infrastructure.Entities.Kanban;
//using ERP.Data.Entities.Planning;
//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.Quality;

//[Table("Quality_NonConformingParts_MasterScrapLog")]
//public class NonConformingPartsMasterScrapLog
//{
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("ScrapID", TypeName = "int")]
//    public int Id { get; set; }

//    [Column("ScrapDate", TypeName = "date")]
//    public DateTime ScrapDate { get; set; }

//    [Column("PartNum", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    [Required]
//    public string PartNumber { get; set; }

//    [Column("Customer", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    [Required]
//    public string Customer { get; set; }

//    [Column("Operation", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Operation { get; set; }

//    [Column("SONumber", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    [Obsolete(
//        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
//    public string SoNumber { get; set; }

//    [Column("Quantity", TypeName = "int")] public int Quantity { get; set; }

//    [Column("CostPerPart", TypeName = "money")]
//    public decimal CostPerPart { get; set; }

//    [Column("Reason1Code", TypeName = "int")]
//    public int Reason1Code { get; set; }

//    [Column("Reason2Code", TypeName = "int")]
//    public int? Reason2Code { get; set; }

//    [Column("Cause1Code", TypeName = "int")]
//    public int Cause1Code { get; set; }

//    [Column("Cause2Code", TypeName = "int")]
//    public int? Cause2Code { get; set; }

//    [Column("ReportedBy", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    [Required]
//    public string ReportedBy { get; set; }

//    [Column("ResponsibleEmp", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string ResponsibleEmployee { get; set; }

//    [Column("Shift", TypeName = "int")] public int? Shift { get; set; }

//    [Column("Material", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Material { get; set; }

//    [Column("TotalScrapCost", TypeName = "money")]
//    public decimal? TotalScrapCost { get; set; }

//    [Column("BlowoutExcessiveScrap", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string BlowoutExcessiveScrap { get; set; }

//    [Column("RMA", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Rma { get; set; }

//    [Column("CPARRequested", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string CparRequested { get; set; }

//    [Column("CPARNum", TypeName = "int")] public int? Cparnum { get; set; }

//    [Column("ExcessiveScrapTotals", TypeName = "money")]
//    public decimal? ExcessiveScrapTotals { get; set; }

//    [Column("RMA Total", TypeName = "money")]
//    public decimal? RmaTotal { get; set; }

//    [Column("RMANum", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Rmanum { get; set; }

//    [Column("QCNote", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    public string Qcnote { get; set; }

//    [Column("QCReviewer", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string QcReviewer { get; set; }

//    [Column("QCActionsTaken", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    public string QcActionsTaken { get; set; }

//    [Column("DepartmentOfError", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string DepartmentOfError { get; set; }

//    [Column("IsAudit", TypeName = "bit")] public bool IsAudit { get; set; }

//    [Column("EnteredBy", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    public string EnteredBy { get; set; }

//    [Column("DateEntered", TypeName = "datetime2(7)")]
//    public DateTime? DateEntered { get; set; }

//    [Column("IsDeleted", TypeName = "bit")]
//    public bool IsDeleted { get; set; }

//    [Column("IsPartsRepairable", TypeName = "bit")]
//    public bool? IsPartsRepairable { get; set; }

//    [Column("ReviewDate", TypeName = "datetime2(7)")]
//    public DateTime? ReviewDate { get; set; }

//    [Column("FeatureNumber", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string FeatureNumber { get; set; }

//    [Column("TicketId", TypeName = "int")] public int? TicketId { get; set; }

//    [Browsable(false)]
//    [NotMapped]
//    [Column("ssmtimestamp", TypeName = "timestamp")]
//    public byte[] ssmtimestamp { get; set; }

//    [ForeignKey("ShopOrder")]
//    [Column("ShopOrderId", TypeName = "int")]
//    public int? ShopOrderId { get; set; }

//    [Column("ScrapQuantityNotes", TypeName = "nvarchar(MAX)")]
//    public string? ScrapQuantityNotes { get; set; }

//    public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
//}