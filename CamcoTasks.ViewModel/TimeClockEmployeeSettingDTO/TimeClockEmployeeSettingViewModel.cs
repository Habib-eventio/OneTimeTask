using System;

namespace CamcoTasks.ViewModels.TimeClockEmployeeSettingDTO
{
    public class TimeClockEmployeeSettingViewModel
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public bool? IsManager { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsRemotelyWorking { get; set; }
        public DateTime? EmailSentDate { get; set; }
        public string ExpectedClockInTime { get; set; }
        public string ExpectedClockOutTime { get; set; }
    }
}
