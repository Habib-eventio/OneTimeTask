// This File Needs to be reviewed Still. Don't Remove this comment.


using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.TimeClock;

namespace CamcoTasks.Infrastructure.IRepository.TimeClock;

public interface IEmployeeSettingRepository : IRepository<EmployeeSetting>
{
    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<string>> GetAllEmployeeIdsByActiveStateAsync(bool isActive);

    /// <summary>
    /// Caution: This Method is being used in AutomatedSystemService Project
    /// </summary>
    Task<List<string>> GetAllEmployeeIdsByEmployeeIdsAsync(List<string> employeeIds);

    /// <summary>
    /// Caution: This Method is being used in Planning, Jarvis Project
    /// </summary>
    //Task<List<EmployeeWeeklyHoursReportViewModel>> GetEmployeeHoursReportAsync(bool isJarvisActive,
    //    DateTime startDateOfWeek, DateTime endDateOfWeek);
}