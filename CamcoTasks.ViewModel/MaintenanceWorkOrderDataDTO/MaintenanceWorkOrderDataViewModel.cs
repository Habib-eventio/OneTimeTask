using System;

namespace CamcoTasks.ViewModels.MaintenanceWorkOrderDataDTO
{
    public class MaintenanceWorkOrderDataViewModel
    {
        public string WorkOrderStatus { get; set; }
        public string Requestor { get; set; }
        public string EquipmentId { get; set; }
        public string Location { get; set; }
        public int? Priority { get; set; }
        public string JobType { get; set; }
        public string ProblemProjectDescriptionAndOrSymptoms { get; set; }
        public DateTime? DateTimeOpen { get; set; }
        public string RemarksActionsTaken { get; set; }
        public DateTime? DateTimeComplete { get; set; }
        public string ManualEntryDowntime { get; set; }
        public string CompletedBy { get; set; }
        public string Parts4Cost { get; set; }
        public string AssignedTo { get; set; }
        public decimal? EstimatedLabor { get; set; }
        public bool IsActive { get; set; }
    }
}
