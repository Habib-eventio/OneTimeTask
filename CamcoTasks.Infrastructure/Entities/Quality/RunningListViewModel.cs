using ERP.Data.Entities.Quality;
using System;
using System.Collections.Generic;

namespace CamcoTasks.Infrastructure.CustomModels.Quality;

public class RunningListViewModel
{
    public int Id { get; set; }

    public string Machine { get; set; }

    public long? OperatorId { get; set; }

    public long? DepartmentId { get; set; }

    public int? MachineId { get; set; }

    public int? InspectionCount { get; set; }

    public string ShopOrderNumber { get; set; }

    public string InspectionStatus { get; set; }

    public string PartNumber { get; set; }

    public string Customer { get; set; }

    public string Department { get; set; }

    public string Operator { get; set; }

    public string Status { get; set; }

    public string OperationNumber { get; set; }

    public int? MachinistWeight { get; set; }

    public int? CustomerWeight { get; set; }

    public int? PartWeight { get; set; }

    public string LastLineInspection { get; set; }

    public int? Total { get; set; }

    public int? TotalWeight { get; set; }

    public int? SequenceNumber { get; set; }

    public bool Active { get; set; }

    public string MachineSetupStyle { get; set; }

    public string MachineRunningStyle { get; set; }

    public string MachineNotRunningStyle { get; set; }

    public DateTime? ChangedTime { get; set; }

    public string MachineDownStyle { get; set; }

    public bool IsAllEmployee { get; set; }

    public string ContainsCheckSheet { get; set; }

    public string ButtonTitle { get; set; }

    public string UpdatedDateTime { get; set; }

    public bool IsGaging { get; set; } = false;

    public int? CustomerAdjustmentId { get; set; }

    public int? QualityIssue { get; set; }

    public int? QualitySpecs { get; set; }

    public int? QualityRequest { get; set; }

    public bool IsNewCustomer { get; set; }

    public int? OrderNumber { get; set; }
        
    //public List<LineInspectionHistoryViewModel> LineInspectionHistory { get; set; }
        
    public DateTime LastInspectedDate { get; set; }
    public string InspectorName { get; set; }
    //public QualityLineInspection InCompleteLineInspection { get; set; }
        
}