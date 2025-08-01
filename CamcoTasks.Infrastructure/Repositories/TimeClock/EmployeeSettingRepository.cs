// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.TimeClock;
using CamcoTasks.Infrastructure.IRepository.TimeClock;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.TimeClock;

public class EmployeeSettingRepository : Repository<EmployeeSetting>,
    IEmployeeSettingRepository
{
    public EmployeeSettingRepository(DatabaseContext context) : base(context)
    {

    }

    private DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public async Task<List<string>> GetAllEmployeeIdsByActiveStateAsync(bool isActive)
    {
        return await DatabaseContext.EmployeeSettings.AsNoTracking()
            .Where(x => x.IsActive.HasValue
                        && x.IsActive == isActive)
            .Select(x => x.EmployeeId)
            .OrderBy(empId => empId)
            .ToListAsync();
    }

    public async Task<List<string>> GetAllEmployeeIdsByEmployeeIdsAsync(List<string> employeeIds)
    {
        return await DatabaseContext.EmployeeSettings.AsNoTracking()
            .Where(x => x.IsActive.HasValue && x.IsActive.Value &&
                        employeeIds.Contains(x.EmployeeId))
            .Select(x => x.EmployeeId)
            .ToListAsync();
    }

    //public async Task<List<EmployeeWeeklyHoursReportViewModel>> GetEmployeeHoursReportAsync(bool isJarvisActive,
    //    DateTime startDateOfWeek, DateTime endDateOfWeek)
    //{
    //    var extraTimeCategories = await DatabaseContext.EmployeeExtraTimeCategories.AsNoTracking().ToListAsync();

    //    #region LINQ Query

    //    var result = (
    //        from employee in await DatabaseContext.Employees.AsNoTracking().ToListAsync()
    //        join employeeSetting in await DatabaseContext.EmployeeSettings.AsNoTracking().ToListAsync() on
    //            employee.CustomEmployeeId equals employeeSetting.EmployeeId
    //        where employee.IsActive && (!isJarvisActive || employeeSetting.IsActive == true)

    //        join approvedRecord in await DatabaseContext.EmployeeApprovedRecords.AsNoTracking()
    //                .Where(x => x.StartDate.Value.Date == startDateOfWeek.Date &&
    //                            x.EndDate.Value.Date == startDateOfWeek.AddDays(6).Date).ToListAsync()
    //            on employee.CustomEmployeeId equals approvedRecord.EmployeeId into approvedRecordGroup

    //        join checkInAndOut in await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
    //                .Where(x => x.CheckInTime.Value.Date >= startDateOfWeek.Date &&
    //                            x.CheckInTime.Value.Date <= endDateOfWeek.Date).ToListAsync()
    //            on employee.CustomEmployeeId equals checkInAndOut.EmployeeId into regularHoursDetailsGroup

    //        join businessTime in await DatabaseContext.BusinessTimes.AsNoTracking()
    //                .Where(x => x.LeftTime.Value.Date >= startDateOfWeek.Date &&
    //                            x.LeftTime.Value.Date <= endDateOfWeek.Date && x.IsDelete != true).ToListAsync()
    //            on employee.CustomEmployeeId equals businessTime.EmployeeId into businessTimeGroup

    //        join vacationHour in await DatabaseContext.VacationRequests.AsNoTracking()
    //                .Where(x => x.IsApproved != null && (bool)x.IsApproved &&
    //                            ((x.FromDate.Value.Date >= startDateOfWeek.Date &&
    //                              x.FromDate.Value.Date <= endDateOfWeek.Date) ||
    //                             (x.ToDate != null && x.ToDate.Value.Date >= startDateOfWeek.Date &&
    //                              x.ToDate.Value.Date <= endDateOfWeek.Date)) &&
    //                            string.IsNullOrEmpty(x.IsDeleteOrEdit))
    //                .OrderBy(x => x.ToDate).ToListAsync()
    //            on employee.CustomEmployeeId equals vacationHour.EmployeeId into vacationHourGroup

    //        join extraTimeRecord in await DatabaseContext.EmployeeExtraTimeRecords.AsNoTracking()
    //                .Where(x => x.ExtraTimeCategoryId != 1 && x.ExtraTimeCategoryId != 7 &&
    //                            x.Date.Value.Date >= startDateOfWeek.Date &&
    //                            x.Date.Value.Date <= endDateOfWeek.Date).ToListAsync()
    //            on employee.Id equals extraTimeRecord.HrEmployeeId into extraTimeRecordGroup

    //        join lostTimeRecord in await DatabaseContext.LostTimeRecords.AsNoTracking()
    //                .Where(x => (x.FromDate.Value.Date >= startDateOfWeek.Date &&
    //                             x.FromDate.Value.Date <= endDateOfWeek.Date) ||
    //                            (x.ToDate != null && x.ToDate.Value.Date >= startDateOfWeek.Date &&
    //                             x.ToDate.Value.Date <= endDateOfWeek.Date)).ToListAsync()
    //            on employee.CustomEmployeeId equals lostTimeRecord.EmployeeId into lostTimeRecordGroup

    //        select new EmployeeWeeklyHoursReportViewModel
    //        {
    //            EmployeeId = employee.CustomEmployeeId,
    //            EmployeeLastName = employee.LastName,
    //            EmployeeFullName = employee.FullName,
    //            ApprovedOrNot = approvedRecordGroup.Any() ? "YES" : "",

    //            RegularHours = Math.Round(
    //                regularHoursDetailsGroup
    //                    .Where(x => x.CheckOutTime.HasValue && x.CheckInTime.HasValue)
    //                    .Select(x =>
    //                        Math.Round(
    //                            (x.CheckOutTime.Value.Hour + (x.CheckOutTime.Value.Minute / 60.0)) -
    //                            (x.CheckInTime.Value.Hour + (x.CheckInTime.Value.Minute / 60.0)),
    //                            1))
    //                    .Where(duration => duration > 0)
    //                    .Sum(),
    //                1) == 0
    //                ? null
    //                : Math.Round(
    //                    regularHoursDetailsGroup
    //                        .Where(x => x.CheckOutTime.HasValue && x.CheckInTime.HasValue)
    //                        .Select(x =>
    //                            Math.Round(
    //                                (x.CheckOutTime.Value.Hour + (x.CheckOutTime.Value.Minute / 60.0)) -
    //                                (x.CheckInTime.Value.Hour + (x.CheckInTime.Value.Minute / 60.0)),
    //                                1))
    //                        .Where(duration => duration > 0)
    //                        .Sum(),
    //                    1),

    //            BusinessTimeHours = Math.Round(
    //                businessTimeGroup
    //                    .Where(x => x.ReturnedTime.HasValue && x.LeftTime.HasValue)
    //                    .Select(x =>
    //                        Math.Round(
    //                            (x.ReturnedTime.Value.Hour + (x.ReturnedTime.Value.Minute / 60.0)) -
    //                            (x.LeftTime.Value.Hour + (x.LeftTime.Value.Minute / 60.0)),
    //                            1))
    //                    .Where(hours => hours > 0)
    //                    .Sum(),
    //                1) == 0
    //                ? null
    //                : Math.Round(
    //                    businessTimeGroup
    //                        .Where(x => x.ReturnedTime.HasValue && x.LeftTime.HasValue)
    //                        .Select(x =>
    //                            Math.Round(
    //                                (x.ReturnedTime.Value.Hour + (x.ReturnedTime.Value.Minute / 60.0)) -
    //                                (x.LeftTime.Value.Hour + (x.LeftTime.Value.Minute / 60.0)),
    //                                1))
    //                        .Where(hours => hours > 0)
    //                        .Sum(),
    //                    1),

    //            VacationHours = vacationHourGroup.Sum(v => v.TotalHours ?? 0) == 0
    //                ? null
    //                : Math.Round(vacationHourGroup.Sum(v => v.TotalHours ?? 0), 1),

    //            LostTimeHours = lostTimeRecordGroup.Sum(x => x.TotalHours ?? 0) == 0
    //                ? null
    //                : Math.Round(lostTimeRecordGroup.Sum(x => x.TotalHours ?? 0), 1),

    //            ExtraTimeCategoriesHours = extraTimeRecordGroup.Sum(x => x.TotalHours ?? 0) == 0
    //                ? null
    //                : Math.Round(extraTimeRecordGroup.Sum(x => x.TotalHours) ?? 0, 1),

    //            VacationHoursDetails = vacationHourGroup.Select(v =>
    //                new EmployeeWeeklyHoursReportViewModel.EmployeeVacationRequests
    //                {
    //                    EmployeeId = employee.CustomEmployeeId,
    //                    TotalHours = v.TotalHours ?? 0,
    //                    Reason = v.Reason,
    //                    FromDate = v.FromDate,
    //                    ToDate = v.ToDate,
    //                    RequestedDate = v.RequestedDate
    //                }).ToList(),

    //            LostTimeHoursDetails = lostTimeRecordGroup.Select(x =>
    //                new EmployeeWeeklyHoursReportViewModel.LostTimeReport
    //                {
    //                    EmployeeId = employee.CustomEmployeeId,
    //                    LostDate = x.ToDate != null
    //                        ? x.FromDate.Value.ToString("MM/dd/yyyy") + " - " +
    //                          x.ToDate.Value.ToString("MM/dd/yyyy")
    //                        : x.FromDate.Value.ToString("MM/dd/yyyy"),
    //                    TotalHours = x.TotalHours,
    //                    Reason = x.Reason
    //                }).ToList(),

    //            RegularHoursDetails = regularHoursDetailsGroup.Select(x =>
    //                new EmployeeWeeklyHoursReportViewModel.EmployeeCheckInOut
    //                {
    //                    EmployeeId = employee.CustomEmployeeId,
    //                    CheckInTime = x.CheckInTime.Value.ToString("hh:mm tt"),
    //                    Date = x.CheckInTime.Value.ToString("MM/dd/yyyy"),
    //                    Day = x.CheckInTime.Value.DayOfWeek.ToString().ToUpper(),
    //                    CheckOutTime = x.CheckOutTime.HasValue ? x.CheckOutTime.Value.ToString("hh:mm tt") : null,
    //                    Hours = x.CheckOutTime.HasValue
    //                        ? Math.Round(
    //                            (Convert.ToDouble(x.CheckOutTime.Value.Hour) +
    //                             (Convert.ToDouble(x.CheckOutTime.Value.Minute) / 60)) -
    //                            (Convert.ToDouble(x.CheckInTime.Value.Hour) +
    //                             (Convert.ToDouble(x.CheckInTime.Value.Minute) / 60)), 1)
    //                        : 0
    //                }).ToList(),

    //            BusinessTimeDetails = businessTimeGroup.Select(x =>
    //                new EmployeeWeeklyHoursReportViewModel.BusinessTime
    //                {
    //                    EmployeeId = employee.CustomEmployeeId,
    //                    LeftTime = x.LeftTime.Value.ToString("hh:mm tt"),
    //                    BusinessDate = x.LeftTime.Value.ToString("MM/dd/yyyy"),
    //                    BusinessDay = x.LeftTime.Value.DayOfWeek.ToString().ToUpper(),
    //                    ReturnedTime = x.ReturnedTime.HasValue ? x.ReturnedTime.Value.ToString("hh:mm tt") : null,
    //                    Hours = x.ReturnedTime.HasValue
    //                        ? Math.Round(
    //                            (Convert.ToDouble(x.ReturnedTime.Value.Hour) +
    //                             (Convert.ToDouble(x.ReturnedTime.Value.Minute) / 60)) -
    //                            (Convert.ToDouble(x.LeftTime.Value.Hour) +
    //                             (Convert.ToDouble(x.LeftTime.Value.Minute) / 60)), 1)
    //                        : 0,
    //                    PlaceOfGoing = x.PlaceOfGoing
    //                }).ToList(),

    //            ExtraTimeCategoriesDetails = extraTimeRecordGroup.Select(x =>
    //                new EmployeeWeeklyHoursReportViewModel.EmployeeExtraTimeRecord
    //                {
    //                    TotalHours = x.TotalHours ?? 0,
    //                    Date = x.Date.HasValue ? x.Date.Value.ToString("MM/dd/yyyy") : null,
    //                    ExtraTimeCategory = extraTimeCategories.Where(y => y.Id == x.ExtraTimeCategoryId)
    //                        .Select(y => y.Field).FirstOrDefault()
    //                }).ToList()
    //        }).OrderBy(x => x.EmployeeFullName).ToList();

    //    #endregion

    //    return result;
    //}
}