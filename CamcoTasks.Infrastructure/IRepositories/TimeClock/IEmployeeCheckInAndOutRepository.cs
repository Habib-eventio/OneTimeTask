// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.TimeClock;

namespace CamcoTasks.Infrastructure.IRepository.TimeClock;

public interface IEmployeeCheckInAndOutRepository : IRepository<EmployeeCheckInAndOut>
{
	/// <summary>
	/// This Method is not being used anywhere.
	/// </summary>
	[Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<List<string>> GetMissingEmployeeOfTheDayAsync(DateTime date);

    /// <summary>
    /// Caution: This Method is being used in Jarvis Project
    /// </summary>
    Task<List<EmployeeCheckInAndOut>> GetIsWorkingEmployeeOfTheDayAsync(DateTime date);

    /// <summary>
    /// Caution: This Method is being used in Jarvis Project
    /// </summary>
    Task<List<EmployeeCheckInAndOut>> GetLeftEmployeeOfTheDayAsync(DateTime date);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<List<EmployeeCheckInAndOut>> GetLateEmployeesAsync(DateTime date);

    /// <summary>
    /// Caution: This Method is being used in Jarvis Project
    /// </summary>
    Task<Tuple<EmployeeCheckInAndOut, string>> GetEmployeeAndCheckInAndOut(
        EmployeeCheckInAndOut employeeCheckInAndOut);

    /// <summary>
    /// Caution: This Method is being used in Human Resource Project
    /// </summary>
    //Task<int> GetMissedClockOutsCountByEmployeeIdAsync(string customEmployeeId);

    /// <summary>
    /// Caution: This Method is being used in AutomatedSystemServiceProject
    /// </summary>
    Task<List<string>> GetDistinctEmployeeIdsByCheckInTimeAndEmployeeIdsAsync(DateTime checkInTime,
        List<string> employeesId);

    /// <summary>
    /// Caution: This Method is being used in AutomatedSystemService Project
    /// </summary>
    Task<EmployeeCheckInAndOut> GetEmployeeCheckInAndOutByEmployeeIdAndCheckInTimeAsync(string employeeId,
        DateTime checkInTime);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
 //   [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	//Task<double> TotalWeekDaysWorkedHoursBefore5pmOfSalaryEmployeesAsync(string employeeId, DateTime startDate,
 //       DateTime endDate);

    /// <summary>
    /// Caution: This Method is being used in AutomatedSystemService Project 
    /// </summary>
    Task<double> TotalWeekEndWorkedHoursOfSalaryEmployeesAsync(string employeeId, DateTime startDate,
        DateTime endDate);

    /// <summary>
    /// Caution: This Method is being used in AutomatedSystemService Project
    /// </summary>
    Task<decimal> TotalOvertimeHoursOfSalaryEmployeesAsync(string employeeId, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Caution: This Method is being used in AutomatedSystemService Project
    /// </summary>
    //Task<double> TotalCamcoHolidayWorkedHoursOfSalaryEmployeesAsync(string employeeId);

	/// <summary>
	/// <summary>
	/// This Method is not being used anywhere.
	/// </summary>
	//[Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	//Task<double> TotalWorkedHoursBefore5pmOfSalaryEmployeeInADayAsync(string employeeId, DateTime startDate,
 //       DateTime endDate, DayOfWeek day);

	/// <summary>
	/// This Method is not being used anywhere.
	/// </summary>
	//[Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	//Task<double> TotalWorkedHoursAfter5pmOfSalaryEmployeeInADayAsync(string employeeId, DateTime startDate,
 //       DateTime endDate, DayOfWeek day);

    /// <summary>
    /// Caution: This Method is being used in AutomatedSystemService Project
    /// </summary>
    //Task<double> TotalWorkedHoursInSpecificTimeOfSalaryEmployeeInADayAsync(string employeeId, DateTime startDate,
    //    DateTime endDate,
    //    DayOfWeek day, TimeSpan reportTime, bool isAfterReportTime);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<List<EmployeeCheckInAndOut>> GetEmployeeCheckInOuts(string employeeId, DateTime startDate,
        DateTime endDate, DayOfWeek day);

    /// <summary>
    /// Caution: This Method is being used in Jarvis Project
    /// </summary>
    //Task<List<MissedOnTimeClockInOutDetailsViewModel>> GetMissedOnTimeClockInOutListAsync(bool isJarvisActive);
}