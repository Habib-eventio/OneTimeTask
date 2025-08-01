using System;

namespace CamcoTasks.ViewModels.ProductionTimeSheetDataDTO
{
    public class ProductionTimeSheetDataViewModel
    {
        public int Id { get; set; }
        public string Employee { get; set; }
        public DateTime? Date { get; set; }
        public string PartNumber { get; set; }
        public string SoNumber { get; set; }
        public string OpNumber { get; set; }
        public string OperationComplete { get; set; }
        public int? BadPcs { get; set; }
        public decimal? PerPc { get; set; }
        public decimal? Total { get; set; }
        public double? CycleTime { get; set; }
        public double? SetUpTime { get; set; }
        public int? FinishedSetups { get; set; }
        public double? BurdenTime { get; set; }
        public double? TotalTime { get; set; }
        public int? ExpenseCode { get; set; }
        public double? ActualCycleTime { get; set; }
        public string SetupComplete { get; set; }
        public decimal? TotalCalculated { get; set; }
        public double? SetupDollars { get; set; }
        public string Note { get; set; }
        public bool HasIssue { get; set; }
        public DateTime? DateEntered { get; set; }
        public int? ProjectId { get; set; }
    }
}
