using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_QualityHolds")]
public partial class QualityHold
{
    [Column("QualityHoldId", TypeName = "int")]
    public int Id { get; set; }

    [Column("QualityHoldNotesId", TypeName = "int")]
    public int? QualityHoldNotesId { get; set; }

    [Column("QualityHoldActionItemsId", TypeName = "int")]
    public int? QualityHoldActionItemsId { get; set; }

    [Column("QualityHoldDate", TypeName = "dateTime")]
    public DateTime QualityHoldDate { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PartNumber { get; set; }

    [Column("Quantity", TypeName = "int")] public int Quantity { get; set; }

    [Column("SO#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ShopOrderNumber { get; set; }

    [Column("CPAR#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Cpar { get; set; }

    [Column("ProblemDescription", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ProblemDescription { get; set; }

    [Column("ResponsibleForNonConformance", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ResponsibleForNonConformance { get; set; }

    [Column("InitiatedBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string InitiatedBy { get; set; }

    [Column("ResponsibleForFollowUp", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ResponsibleForFollowUp { get; set; }

    [Column("RemovedFromQualityHoldDate", TypeName = "dateTime")]
    public DateTime? RemovedFromQualityHoldDate { get; set; }

    [Column("OutForQualityHoldTransactionMadeBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string OutForQualityHoldTransactionMadeBy { get; set; }

    [Column("InFromQualityHoldTransactionMadeBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string InFromQualityHoldTransactionMadeBy { get; set; }

    [Column("RMA#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Rma { get; set; }

    [Column("Disposition", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Disposition { get; set; }

    [Column("StockPullRequired", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string StockPullRequired { get; set; }

    [Column("RemovedBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RemovedBy { get; set; }

    [Column("RepairSO#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RepairShopOrderNumber { get; set; }

    [Column("Machine", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Machine { get; set; }

    [Column("CurrentLocation", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CurrentLocation { get; set; }

    [Column("QuantityRemoved", TypeName = "int")]
    public int? QuantityRemoved { get; set; }

    [Column("RemovalAuthorizedBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RemovalAuthorizedBy { get; set; }

    [Column("Operation#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Operation { get; set; }

    [Column("Revision", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Revision { get; set; }

    [Column("MethodsChampion", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string MethodsChampion { get; set; }

    [Column("QualityHoldActionItem", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string QualityHoldActionItem { get; set; }

    [Column("NoteAddedby1", TypeName = "int")]
    public int? NoteAddedBy { get; set; }

    [Column("DepartmentResposible", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string DepartmentResponsible { get; set; }

    [Column("Totaltime", TypeName = "dateTime")]
    public DateTime? TotalTime { get; set; }

    [Column("CriticalHold", TypeName = "int")]
    public int? CriticalHold { get; set; }

    [Browsable(false)]
    [NotMapped]
    [Column("SSMA_TimeStamp", TypeName = "timestamp")]
    public byte[] SsmaTimeStamp { get; set; }
}