// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Quality;

[Table("Quality_LineInspections")]
public class QualityLineInspection
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("InspectionAuditId", TypeName = "int")]
    public int Id { get; set; }

    [Column("OperatorId", TypeName = "nvarchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string OperatorId { get; set; }

    [Column("ShopNumber", TypeName = "nvarchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    public string ShopNumber { get; set; }

    [Column("AuditDate", TypeName = "datetime2(7)")]
    public DateTime AuditDate { get; set; }

    [Column("Path", TypeName = "nvarchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string Path { get; set; }

    [Column("IsPass", TypeName = "bit")] public bool? IsPass { get; set; }

    [Column("DateModified", TypeName = "datetime2(7)")]
    public DateTime DateModified { get; set; }

    [Column("EnterById", TypeName = "nvarchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string EnterById { get; set; }

    [Column("DateAdded", TypeName = "datetime2(7)")]
    public DateTime DateAdded { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("InspectorId", TypeName = "nvarchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    public string InspectorId { get; set; }

    [Column("InspectionStatus", TypeName = "bit")]
    public bool InspectionStatus { get; set; }

    [Column("Reason", TypeName = "varchar(200)")]
    [StringLength(200)]
    [MaxLength(200)]
    public string Reason { get; set; }

    [Column("PartNumber", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string PartNumber { get; set; }

    [Column("IsShopNumberFaulty", TypeName = "bit")]
    public bool IsShopNumberFaulty { get; set; }

    [Column("IsInspectionDateFaulty", TypeName = "bit")]
    public bool IsInspectionDateFaulty { get; set; }

    [Column("IsReviewed", TypeName = "bit")]
    public bool? IsReviewed { get; set; }

    [Column("IsOperatorNameFaulty", TypeName = "bit")]
    public bool IsOperatorNameFaulty { get; set; }

    [Column("IsInspectorNameFaulty", TypeName = "bit")]
    public bool IsInspectorNameFaulty { get; set; }

    [Column("ModifiedById", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    public string ModifiedById { get; set; }

    [Column("Shift", TypeName = "int")] public int? Shift { get; set; }

    [Column("IsLineInspection", TypeName = "bit")]
    public bool? IsLineInspection { get; set; }

    [Column("IsProductionHoldOrDelay", TypeName = "bit")]
    public bool? IsProductionHoldOrDelay { get; set; }

    [Column("IsPerProductionSupervisor", TypeName = "bit")]
    public bool? IsPerProductionSupervisor { get; set; }

    [Column("IsSameDayShip", TypeName = "bit")]
    public bool? IsSameDayShip { get; set; }

    [Column("IsHotList", TypeName = "bit")]
    public bool? IsHotList { get; set; }

    [Column("IsOtherItems", TypeName = "bit")]
    public bool? IsOtherItems { get; set; }

    [Column("ReasonForOtherItem", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ReasonForOtherItem { get; set; }

    [Column("IsSetupSheet", TypeName = "bit")]
    public bool? IsSetupSheet { get; set; }

    [Column("IsCheckSheet", TypeName = "bit")]
    public bool? IsCheckSheet { get; set; }

    [Column("IsOds", TypeName = "bit")] public bool? IsOds { get; set; }

    [Column("IsGageCalibrated", TypeName = "bit")]
    public bool? IsGageCalibrated { get; set; }

    [Column("IsGagingWithLessTolerance", TypeName = "bit")]
    public bool? IsGagingWithLessTolerance { get; set; }

    [Column("IsUnacceptablePartFinish", TypeName = "bit")]
    public bool? IsUnacceptablePartFinish { get; set; }

    [Column("IsUnacceptableSharpEdges", TypeName = "bit")]
    public bool? IsUnacceptableSharpEdges { get; set; }

    [Column("NonConformingComments", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string NonConformingComments { get; set; }

    [Column("NonConformanceResultId", TypeName = "int")]
    public int? NonConformanceResultId { get; set; }

    [Column("IsComplete", TypeName = "bit")]
    public bool? IsComplete { get; set; }

    [Column("PartRunningId", TypeName = "int")]
    public int? PartRunningId { get; set; }

    [Column("TimeSpent", TypeName = "float")]
    public double? TimeSpent { get; set; }

    [Column("MachineId", TypeName = "int")]
    public int? MachineId { get; set; }

    [Column("DepartmentId", TypeName = "int")]
    public int? DepartmentId { get; set; }

    [Column("JobTraveler", TypeName = "nvarchar(10)")]
    [StringLength(10)]
    [MaxLength(10)]
    public string JobTraveler { get; set; }

    [Column("OperationNumber", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string OperationNumber { get; set; }

    [Column("DropOffTime", TypeName = "datetime2(7)")]
    public DateTime? DropOffTime { get; set; }

    [Column("CompletedTime", TypeName = "datetime2(7)")]
    public DateTime? CompletedTime { get; set; }

    [Column("ReasonOfFailure", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ReasonOfFailure { get; set; }

    [Column("IsCmmRequired", TypeName = "bit")]
    public bool IsCmmRequired { get; set; }

    [Column("IsProcessProveOut", TypeName = "bit")]
    public bool IsProcessProveOut { get; set; }

    [Column("IsInspectionPaused", TypeName = "bit")]
    public bool IsInspectionPaused { get; set; }

    [Column("PauseStartTime", TypeName = "datetime2(7)")]
    public DateTime? PauseStartTime { get; set; }

    [Column("PausedReason", TypeName = "NVARCHAR(MAX)")]
    public string PausedReason { get; set; }

    //public virtual ICollection<LineInspectionTimeDurationLog> LineInspectionTimeDurationLogs { get; set; }

    //public QualityLineInspection()
    //{
    //    LineInspectionTimeDurationLogs = new List<LineInspectionTimeDurationLog>();
    //}
}