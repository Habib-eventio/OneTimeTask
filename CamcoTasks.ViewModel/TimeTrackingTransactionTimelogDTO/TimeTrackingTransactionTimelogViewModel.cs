using System;

namespace CamcoTasks.ViewModels.TimeTrackingTransactionTimelogDTO
{
    public class TimeTrackingTransactionTimelogViewModel
    {
        public int Id { get; set; }
        public int? GenericId { get; set; }
        public int? TransactionId { get; set; }
        public int CategoryId { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int? Difference { get; set; }
        public int? TimeTaken { get; set; }
        public string? PageName { get; set; }
        public string? IsPaused { get; set; }
        public bool? IsComplete { get; set; }
        public string? TransactionBy { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
        public string? Description2 { get; set; }
        public string? Value2 { get; set; }
        public bool? IsCancel { get; set; }
        public bool? IsFlagged { get; set; }
        public string? FlaggedNote { get; set; }
        public DateTime? EnteredDate { get; set; }
    }
}
