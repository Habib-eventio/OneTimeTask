using System;

namespace CamcoTasks.ViewModels.LoginLogs
{
    public class LoginLogsViewModel
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public DateTime SignedInTime { get; set; }
        public DateTime? SignedOutTime { get; set; }
        public int? TotalChanges { get; set; }
        public short? ApplicationId { get; set; }
    }
}
