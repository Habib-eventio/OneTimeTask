using CamcoTasks.Infrastructure.Entities.Task;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IRecurringTaskReportService
    {
        Task IncressUpcommicDate(string frequency, int incressDay);
        Task CheckPastDueTasksAsync(string appUrl);
        Task CheckPastDueTasksAsync(DateTime reportDate);
        Task SendWarningMailBeforeDeactiveTaskAsync(string appUrl);
        Task DeactivateTasksAsync();
        Task<bool> SendRecurringTaskAuditReportAsync(DateTime reapotingDate);
        double TotalSpentTime(IEnumerable<UpdateReportViewModel> updateTeportList, int timeFormat);
        Task<IEnumerable<UpdateReportViewModel>> RecTaskPastAverageTime(IEnumerable<UpdateReportViewModel> updateReportList);
        Task DeactiveRecurringTasksReportAsync();
    }
}
