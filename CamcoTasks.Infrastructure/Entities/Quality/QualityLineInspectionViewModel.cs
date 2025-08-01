using System;

namespace ERP.Data.CustomModels.Quality;

public class QualityLineInspectionViewModel
{
    public int InspectionAuditId { get; set; }

    public string OperatorId { get; set; }

    public string OperatorName { get; set; }

    public string InspectorId { get; set; }

    public string InspectorName { get; set; }

    public string ShopNumber { get; set; }

    public string PartNumber { get; set; }

    public string Customer { get; set; }

    public string OperationNumber { get; set; }

    public string Machine { get; set; }

    public string Department { get; set; }

    public int Shift { get; set; }

    public DateTime? DateAndTime { get; set; }

    public DateTime AddedDate { get; set; }

    public DateTime? CompletedTime { get; set; }

    public DateTime? DropOffTime { get; set; }

    public DateTime? CompletedDate { get; set; }

    public DateTime? DropOffDate { get; set; }

    public bool InspectionStatus { get; set; }

    public bool? IsLineInspection { get; set; }

    public bool? IsProductionHoldOrDelay { get; set; }

    public bool? IsPerProductionSupervisor { get; set; }

    public bool? IsSameDayShip { get; set; }

    public bool? IsHotList { get; set; }

    public bool? IsOtherItems { get; set; }

    public bool? IsPass { get; set; }

    public string ReasonForOtherItem { get; set; }

    public bool? IsSetupSheet { get; set; }

    public bool? IsCheckSheet { get; set; }

    public bool? IsOds { get; set; }

    public bool? IsComplete { get; set; }

    public bool? IsGageCalibrated { get; set; }

    public bool? IsGagingWithLessTolerance { get; set; }

    public bool? IsUnacceptablePartFinish { get; set; }

    public bool? IsUnacceptableSharpEdges { get; set; }

    public string NonConformingComments { get; set; }

    public int? NonConformanceResultId { get; set; }

    public int? PartRunningId { get; set; }

    public double? TimeSpent { get; set; }

    public int? MachineId { get; set; }

    public int? DepartmentId { get; set; }

    public string JobTraveler { get; set; }

    public string LineInspectionStatus { get; set; }

    public TimeSpan? TimeDuration { get; set; }

    public string ReasonOfFailure { get; set; }

    public string InspectionType => IsLineInspection == true
        ? "LINE INSPECTION"
        : "FIRST PIECE";

    public bool IsCmmRequired { get; set; }

    public bool IsProcessProveOut { get; set; }


}