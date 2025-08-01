using System;

namespace CamcoTasks.ViewModels.TimeClock_EmployeeExtraTimeRecordDTO
{
    public class TimeClock_EmployeeExtraTimeRecordViewModel
    {
        public int ExtraTimeId { get; set; }
        public DateTime? Date { get; set; }
        public int? ExtraTimeCategoryId { get; set; }
        public long EmployeeId { get; set; }
        public double? TotalHours { get; set; }
        public int? EntryId { get; set; }
    }
}
