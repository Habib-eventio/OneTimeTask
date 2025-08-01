// This File Needs to be reviewed Still. Don't Remove this comment.
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Production;

[Table("Production_TimeSheetData")]
public class TimeSheetDatum
{
    [Column("Employee", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Employee { get; set; }

    [Column("Date", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column("Part Number", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PartNumber { get; set; }

    [Column("SO Number", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public string ShopOrderNumber { get; set; }

    [Column("Op Number", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string OperationNumber { get; set; }

    [Column("Operation Complete", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string OperationComplete { get; set; }

    [Column("Order Qty", TypeName = "int")]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public int? OrderQuantity { get; set; }

    [Column("Bad Pcs", TypeName = "int")] public int? BadPieces { get; set; }

    [Column("Total Qty", TypeName = "int")]
    public int? TotalQuantity { get; set; }

    [Column("$$ Per Pc", TypeName = "money")]
    public decimal? PricePerPiecesInDollars { get; set; }

    [Column("$$ Total", TypeName = "money")]
    public decimal? TotalPriceInDollars { get; set; }

    [Column("Cycle Time", TypeName = "float")]
    public double? CycleTime { get; set; }

    [Column("Set-Up Time", TypeName = "float")]
    public double? SetUpTime { get; set; }

    [Column("FINISHED SETUPS", TypeName = "int")]
    public int? FinishedSetups { get; set; }

    [Column("Burden Time", TypeName = "float")]
    public double? BurdenTime { get; set; }

    [Column("Total Time", TypeName = "float")]
    public double? TotalTime { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("DepartmentId", TypeName = "bigint")]
    public long? DepartmentId { get; set; }

    [Column("ExpenseCode", TypeName = "int")]
    public int? ExpenseCode { get; set; }

    [Column("ActualCycleTime", TypeName = "float")]
    public double? ActualCycleTime { get; set; }

    [Column("SetupComplete", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string SetupComplete { get; set; }

    [Column("$$ Total Calculated", TypeName = "money")]
    public decimal? TotalCalculated { get; set; }

    [Column("SetupDollars", TypeName = "float")]
    public double? SetupDollars { get; set; }

    [Column("Note", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Note { get; set; }

    [Column("HasIssue", TypeName = "bit")] public bool HasIssue { get; set; }

    [Column("DateEntered", TypeName = "datetime2(7)")]
    public DateTime? DateEntered { get; set; }

    [Column("ItticketId", TypeName = "int")]
    public int? ITTicketId { get; set; }

    [Column("MaintenanceWorkOrderId", TypeName = "int")]
    public int? MaintenanceWorkOrderId { get; set; }

    [Column("ProjectId", TypeName = "int")]
    public int? ProjectId { get; set; }

    [Column("RecurringTaskID", TypeName = "int")]
    public int? RecurringTaskId { get; set; }

    [ForeignKey("ShopOrder")]
    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }

    [Column("IsActive", TypeName = "bit")]
    public bool IsActive { get; set; }

    //public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
}