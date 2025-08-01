// This File Needs to be reviewed Still. Don't Remove this comment.


using CamcoTasks.Infrastructure.Entities.TimeClock;
using CamcoTasks.Infrastructure.IRepository.TimeClock;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.TimeClock;

public class EmployeeCheckInAndOutRepository : Repository<EmployeeCheckInAndOut>,
	IEmployeeCheckInAndOutRepository
{
	public EmployeeCheckInAndOutRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<List<EmployeeCheckInAndOut>> GetIsWorkingEmployeeOfTheDayAsync(DateTime date)
	{
		var query = await (from checkInOut in DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
						   where checkInOut.CheckInTime.Value.Date == date.Date
						   group checkInOut by checkInOut.EmployeeId
				into empGroup
						   select empGroup.OrderByDescending(c => c.CheckInTime).FirstOrDefault())
			.ToListAsync();

		return query.Where(x => x.CheckOutTime == null)
			.ToList();

	}

	public async Task<List<string>> GetMissingEmployeeOfTheDayAsync(DateTime date)
	{
		var notPresentQuery = from employee in DatabaseContext.Employees.AsNoTracking()
							  where !(
									  from checkInAndOut in DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
									  where checkInAndOut.CheckInTime.Value.Date == date.Date
									  select checkInAndOut.EmployeeId
								  ).Distinct()
								  .Contains(employee.CustomEmployeeId)
							  select employee.CustomEmployeeId;

		return await (from employee in DatabaseContext.Employees.AsNoTracking()
					  join notPresent in notPresentQuery
						  on employee.CustomEmployeeId equals notPresent
					  where (employee.ShiftStartTime.Value.TimeOfDay <= date.TimeOfDay &&
							 employee.ShiftEndTime.Value.TimeOfDay >= date.TimeOfDay) ||
							employee.ShiftEndTime.Value.TimeOfDay < date.TimeOfDay ||
							employee.ShiftEndTime == null ||
							employee.ShiftStartTime == null
					  select employee.CustomEmployeeId).ToListAsync();
	}

	public async Task<List<EmployeeCheckInAndOut>> GetLeftEmployeeOfTheDayAsync(DateTime date)
	{
		var query = await (from checkInOut in DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
						   where checkInOut.CheckInTime.Value.Date == date.Date
						   group checkInOut by checkInOut.EmployeeId
				into empGroup
						   select empGroup.OrderByDescending(c => c.CheckInTime).FirstOrDefault())
			.ToListAsync();

		return query.Where(x => x.CheckOutTime != null)
			.ToList();
	}

	public async Task<List<EmployeeCheckInAndOut>> GetLateEmployeesAsync(DateTime date)
	{
		var query = await (from checkTimeRecord in (
					from checkInAndOut in DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
					where checkInAndOut.CheckInTime.Value.Date == date.Date
					group checkInAndOut by new { checkInAndOut.EmployeeId }
					into grouped
					select new
					{
						grouped.Key.EmployeeId,
						FirstCheckInTime = grouped.OrderBy(c => c.CheckInTime.Value.Date).Select(x => x.CheckInTime)
							.FirstOrDefault()
					}
				).Where(checkTimeRecord => checkTimeRecord.FirstCheckInTime != null)
						   join t2 in DatabaseContext.Employees.AsNoTracking() on checkTimeRecord.EmployeeId equals t2
							   .CustomEmployeeId
						   where
							   (checkTimeRecord.FirstCheckInTime.Value.Hour > t2.ShiftStartTime.Value.Hour) ||
							   (
								   (checkTimeRecord.FirstCheckInTime.Value.Hour == t2.ShiftStartTime.Value.Hour) &&
								   (checkTimeRecord.FirstCheckInTime.Value.Minute > t2.ShiftStartTime.Value.Minute)
							   )
						   select new EmployeeCheckInAndOut
						   {
							   EmployeeId = checkTimeRecord.EmployeeId,
							   CheckInTime = checkTimeRecord.FirstCheckInTime
						   }
			).ToListAsync();

		return query;
	}

	public async Task<Tuple<EmployeeCheckInAndOut, string>> GetEmployeeAndCheckInAndOut(
		EmployeeCheckInAndOut employeeCheckInAndOut)
	{
		var query = (from checkInOut in DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
					 join emp in DatabaseContext.Employees.AsNoTracking() on checkInOut.EmployeeId equals emp
						 .CustomEmployeeId
					 where (checkInOut.Id == employeeCheckInAndOut.Id)
					 select new Tuple<EmployeeCheckInAndOut, string>(
						 new EmployeeCheckInAndOut
						 {
							 Id = checkInOut.Id,
							 EmployeeId = checkInOut.EmployeeId,
							 CheckInTime = checkInOut.CheckInTime,
							 CheckOutTime = checkInOut.CheckOutTime,
							 CheckInImage = checkInOut.CheckInImage,
							 CheckOutImage = checkInOut.CheckOutImage,
							 CheckInMacAddress = checkInOut.CheckInMacAddress,
							 CheckOutMacAddress = checkInOut.CheckOutMacAddress,
							 IsRemotelyCheckIn = checkInOut.IsRemotelyCheckIn,
							 IsRemotelyCheckOut = checkInOut.IsRemotelyCheckOut,
							 DateCreated = checkInOut.DateCreated,
							 CreatedById = checkInOut.CreatedById,
							 IsAutomaticCheckIn = checkInOut.IsAutomaticCheckIn,
							 IsAutomaticCheckOut = checkInOut.IsAutomaticCheckOut,
							 IsUpworkCheckIn = checkInOut.IsUpworkCheckIn,
							 IsUpworkCheckOut = checkInOut.IsUpworkCheckOut
						 },
						 (emp.LastName + ", " + emp.FirstName)
					 )).FirstOrDefault();
		return query;
	}

	//public async Task<int> GetMissedClockOutsCountByEmployeeIdAsync(string employeeId)
	//{
	//	var notSubmittingRequestClockOut = await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
	//		.Where(x => x.EmployeeId == employeeId && x.CheckOutTime == null).ToListAsync();
	//	var reqList = await DatabaseContext.CheckInOutRequests.AsNoTracking().Where(x =>
	//		x.EmployeeId == employeeId && x.OldCheckOutTime == null && x.RequestedCheckOutTime != null &&
	//		x.RequestedCheckInTime == null).ToListAsync();
	//	return notSubmittingRequestClockOut.Count + reqList.Count;
	//}

	public async Task<EmployeeCheckInAndOut> GetEmployeeCheckInAndOutByEmployeeIdAndCheckInTimeAsync(
		string employeeId, DateTime checkInTime)
	{
		return await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
			.Where(x => x.CheckInTime.Value.Date == DateTime.Now.Date
						&& x.CheckOutTime == null && x.EmployeeId == employeeId)
			.OrderByDescending(x => x.CheckInTime)
			.FirstOrDefaultAsync();
	}

	public async Task<List<string>> GetDistinctEmployeeIdsByCheckInTimeAndEmployeeIdsAsync(DateTime checkInTime,
		List<string> employeesId)
	{
		return await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
			.Where(x => x.CheckInTime.Value.Date == checkInTime.Date && x.CheckOutTime == null &&
						employeesId.Contains(x.EmployeeId)).Select(x => x.EmployeeId).Distinct().ToListAsync();
	}

	//public async Task<double> TotalWeekDaysWorkedHoursBefore5pmOfSalaryEmployeesAsync(string employeeId,
	//	DateTime startDate, DateTime endDate)
	//{
	//	TimeSpan reportTime = new TimeSpan(17, 0, 0);
	//	var camcoHolidaysDates = new List<DateTime?>();
	//	var holidayDates = DatabaseContext.CamcoHolidayDates.AsNoTracking()
	//		.Select(x => new { x.FromDate, x.ToDate }).ToList();
	//	foreach (var holiday in holidayDates)
	//	{
	//		if (holiday.ToDate != null)
	//		{
	//			for (DateTime date = (DateTime)holiday.FromDate;
	//				 date <= holiday.ToDate.Value;
	//				 date = date.AddDays(1))
	//			{
	//				camcoHolidaysDates.Add(date);
	//			}
	//		}
	//		else
	//		{
	//			camcoHolidaysDates.Add(holiday.FromDate);
	//		}
	//	}

	//	var totalWeekDaysWorkedHours = (await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
	//			.Where(entry => entry.EmployeeId == employeeId
	//							&& entry.CheckInTime != null && entry.CheckOutTime != null
	//							&& entry.CheckInTime.Value.Date >= startDate.Date
	//							&& entry.CheckInTime.Value.Date <= endDate.Date)
	//			.Select(entry => new
	//			{
	//				entry.CheckInTime,
	//				entry.CheckOutTime
	//			})
	//			.ToListAsync())
	//		.Where(entry => entry.CheckInTime.Value.Hour < 17
	//						&& (entry.CheckInTime.Value.DayOfWeek == DayOfWeek.Monday
	//							|| entry.CheckInTime.Value.DayOfWeek == DayOfWeek.Tuesday
	//							|| entry.CheckInTime.Value.DayOfWeek == DayOfWeek.Wednesday
	//							|| entry.CheckInTime.Value.DayOfWeek == DayOfWeek.Thursday
	//							|| entry.CheckInTime.Value.DayOfWeek == DayOfWeek.Friday)
	//						&& !camcoHolidaysDates.Contains(entry.CheckInTime.Value.Date))
	//		.Select(entry => new
	//		{
	//			Hours = entry.CheckOutTime.Value.Hour > 17
	//				? (reportTime - entry.CheckInTime.Value.TimeOfDay).TotalHours
	//				: (entry.CheckOutTime - entry.CheckInTime).Value.TotalHours
	//		})
	//		.Sum(result => result.Hours);
	//	return totalWeekDaysWorkedHours;
	//}

	//public async Task<double> TotalWorkedHoursBefore5pmOfSalaryEmployeeInADayAsync(string employeeId,
	//	DateTime startDate, DateTime endDate, DayOfWeek day)
	//{
	//	TimeSpan reportTime = new TimeSpan(17, 0, 0);

	//	var camcoHolidaysDates = new List<DateTime?>();
	//	var holidayDates = DatabaseContext.CamcoHolidayDates.AsNoTracking()
	//		.Select(x => new { x.FromDate, x.ToDate })
	//		.ToList();

	//	foreach (var holiday in holidayDates)
	//	{
	//		if (holiday.ToDate != null)
	//		{
	//			for (DateTime date = (DateTime)holiday.FromDate;
	//				 date <= holiday.ToDate.Value;
	//				 date = date.AddDays(1))
	//			{
	//				camcoHolidaysDates.Add(date);
	//			}
	//		}
	//		else
	//		{
	//			camcoHolidaysDates.Add(holiday.FromDate);
	//		}
	//	}

	//	var employeeCheckInAndOutEntries = (await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
	//			.Where(x => x.EmployeeId == employeeId
	//						&& x.CheckInTime != null
	//						&& x.CheckOutTime != null
	//						&& x.CheckInTime.Value >= startDate
	//						&& x.CheckOutTime.Value <= endDate)
	//			.ToListAsync())
	//		.Select(entry => new
	//		{
	//			entry.CheckInTime,
	//			entry.CheckOutTime
	//		})
	//		.ToList()
	//		.Where(entry => entry.CheckInTime.Value.DayOfWeek == day)
	//		.ToList();

	//	var totalWeekDaysWorkedHours = employeeCheckInAndOutEntries
	//		.Where(entry => entry.CheckInTime.Value.TimeOfDay < reportTime
	//						&& !camcoHolidaysDates.Contains(entry.CheckInTime.Value.Date))
	//		.Select(entry => new
	//		{
	//			Hours = entry.CheckOutTime.Value.TimeOfDay > reportTime
	//				? (reportTime - entry.CheckInTime.Value.TimeOfDay).TotalHours
	//				: (entry.CheckOutTime - entry.CheckInTime).Value.TotalHours
	//		})
	//		.Sum(result => result.Hours);


	//	return totalWeekDaysWorkedHours;
	//}

	//public async Task<double> TotalWorkedHoursAfter5pmOfSalaryEmployeeInADayAsync(string employeeId,
	//	DateTime startDate, DateTime endDate, DayOfWeek day)
	//{
	//	TimeSpan reportTime = new TimeSpan(17, 0, 0);

	//	var camcoHolidaysDates = new List<DateTime?>();
	//	var holidayDates = DatabaseContext.CamcoHolidayDates.AsNoTracking()
	//		.Select(x => new { FromDate = x.FromDate, ToDate = x.ToDate })
	//		.ToList();

	//	foreach (var holiday in holidayDates)
	//	{
	//		if (holiday.ToDate != null)
	//		{
	//			for (DateTime date = (DateTime)holiday.FromDate;
	//				 date <= holiday.ToDate.Value;
	//				 date = date.AddDays(1))
	//			{
	//				camcoHolidaysDates.Add(date);
	//			}
	//		}
	//		else
	//		{
	//			camcoHolidaysDates.Add(holiday.FromDate);
	//		}
	//	}

	//	var employeeCheckInAndOutEntries = (await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
	//			.Where(x => x.EmployeeId == employeeId
	//						&& x.CheckInTime != null
	//						&& x.CheckOutTime != null
	//						&& x.CheckInTime.Value >= startDate
	//						&& x.CheckOutTime.Value <= endDate)
	//			.ToListAsync())
	//		.Select(entry => new
	//		{
	//			entry.CheckInTime,
	//			entry.CheckOutTime
	//		})
	//		.ToList()
	//		.Where(entry => entry.CheckInTime.Value.DayOfWeek == day)
	//		.ToList();

	//	var totalWeekDaysWorkedHours = employeeCheckInAndOutEntries
	//		.Where(entry => entry.CheckOutTime.Value.TimeOfDay > reportTime
	//						&& !camcoHolidaysDates.Contains(entry.CheckInTime.Value.Date))
	//		.Select(entry => new
	//		{
	//			Hours = entry.CheckInTime.Value.TimeOfDay < reportTime
	//				? (entry.CheckOutTime.Value.TimeOfDay - reportTime).TotalHours
	//				: (entry.CheckOutTime - entry.CheckInTime).Value.TotalHours
	//		})
	//		.Sum(result => result.Hours);


	//	return totalWeekDaysWorkedHours;
	//}

	//private Task<List<DateTime?>> GetCamcoHolidayDatesAsync()
	//{
	//	var camcoHolidaysDates = new List<DateTime?>();
	//	var holidayDates = DatabaseContext.CamcoHolidayDates.AsNoTracking()
	//		.Select(x => new { x.FromDate, x.ToDate })
	//		.ToList();

	//	foreach (var holiday in holidayDates)
	//	{
	//		if (holiday.ToDate != null)
	//		{
	//			for (DateTime date = (DateTime)holiday.FromDate;
	//			     date <= holiday.ToDate.Value;
	//			     date = date.AddDays(1))
	//			{
	//				camcoHolidaysDates.Add(date);
	//			}
	//		}
	//		else
	//		{
	//			camcoHolidaysDates.Add(holiday.FromDate);
	//		}
	//	}

	//	return Task.FromResult(camcoHolidaysDates);
	//}

	public async Task<List<EmployeeCheckInAndOut>> GetEmployeeCheckInOuts(string employeeId, DateTime startDate,
		DateTime endDate, DayOfWeek day)
	{

		var employeeCheckInAndOut = (await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
				.Where(x => x.EmployeeId == employeeId
							&& x.CheckInTime != null
							&& x.CheckOutTime != null
							&& x.CheckInTime.Value.Date >= startDate
							&& x.CheckOutTime.Value.Date <= endDate)
				.ToListAsync())
			.Where(entry => entry.CheckInTime.Value.DayOfWeek == day)
			.ToList();

		return employeeCheckInAndOut.ToList();
	}

	//public async Task<double> TotalWorkedHoursInSpecificTimeOfSalaryEmployeeInADayAsync(string employeeId,
	//	DateTime startDate, DateTime endDate, DayOfWeek day, TimeSpan reportTime, bool isAfterReportTime)
	//{
	//	var camcoHolidaysDates = await GetCamcoHolidayDatesAsync();
	//	var employeeCheckInAndOutEntries = await GetEmployeeCheckInOuts(employeeId, startDate, endDate, day);
	//	List<EmployeeCheckInAndOut> filteredEmployeeCheckInAndOutEntries;
	//	if (isAfterReportTime)
	//	{
	//		filteredEmployeeCheckInAndOutEntries = employeeCheckInAndOutEntries.Where(entry =>
	//			entry.CheckOutTime.Value.TimeOfDay > reportTime
	//			&& !camcoHolidaysDates.Contains(entry.CheckInTime.Value.Date)).ToList();
	//		return filteredEmployeeCheckInAndOutEntries.Select(entry => new
	//		{
	//			Hours = entry.CheckInTime.Value.TimeOfDay < reportTime
	//					? (entry.CheckOutTime.Value.TimeOfDay - reportTime).TotalHours
	//					: (entry.CheckOutTime - entry.CheckInTime).Value.TotalHours
	//		})
	//			.Sum(result => result.Hours);
	//	}
	//	else
	//	{
	//		filteredEmployeeCheckInAndOutEntries = employeeCheckInAndOutEntries.Where(entry =>
	//			entry.CheckInTime.Value.TimeOfDay < reportTime
	//			&& !camcoHolidaysDates.Contains(entry.CheckInTime.Value.Date)).ToList();
	//		return filteredEmployeeCheckInAndOutEntries.Select(entry => new
	//		{
	//			Hours = entry.CheckOutTime.Value.TimeOfDay > reportTime
	//					? (reportTime - entry.CheckInTime.Value.TimeOfDay).TotalHours
	//					: (entry.CheckOutTime - entry.CheckInTime).Value.TotalHours
	//		})
	//			.Sum(result => result.Hours);
	//	}
	//}

	public async Task<double> TotalWeekEndWorkedHoursOfSalaryEmployeesAsync(string employeeId, DateTime startDate,
		DateTime endDate)
	{
		var totalWeekEndWorkedHours = (await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
				.Where(entry => entry.EmployeeId == employeeId
								&& entry.CheckInTime != null && entry.CheckOutTime != null
								&& entry.CheckInTime.Value.Date >= startDate.Date
								&& entry.CheckInTime.Value.Date <= endDate.Date)
				.ToListAsync())
			.Where(entry => entry.CheckInTime.Value.DayOfWeek == DayOfWeek.Sunday ||
							entry.CheckInTime.Value.DayOfWeek == DayOfWeek.Saturday)
			.Select(entry => new
			{
				TotalWorkedHours = (entry.CheckOutTime.Value - entry.CheckInTime.Value).TotalHours
			})
			.Sum(result => result.TotalWorkedHours);

		return totalWeekEndWorkedHours;
	}

	public async Task<decimal> TotalOvertimeHoursOfSalaryEmployeesAsync(string employeeId, DateTime startDate,
		DateTime endDate)
	{
		var overtime = (await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
				.Join(DatabaseContext.Employees.AsNoTracking(),
					empCIO => empCIO.EmployeeId,
					employee => employee.CustomEmployeeId,
					(empCIO, employee) => new { empCIO, employee })
				.Where(joinResult => joinResult.employee.CustomEmployeeId == employeeId &&
									 joinResult.empCIO.CheckInTime != null &&
									 joinResult.empCIO.CheckOutTime != null &&
									 joinResult.empCIO.CheckInTime.Value.Date >= startDate.Date &&
									 joinResult.empCIO.CheckInTime.Value.Date <= endDate.Date).ToListAsync())
			.GroupBy(joinResult => joinResult.employee.CustomEmployeeId)
			.Select(group => new
			{
				OverTime = (decimal)(group.Sum(item =>
							   (item.empCIO.CheckOutTime - item.empCIO.CheckInTime).Value.TotalSeconds) / 3600.0) -
						   group.Max(item => item.employee.NormalWorkHours)
			})
			.FirstOrDefault();

		return (overtime?.OverTime ?? 0);
	}

	//	public async Task<double> TotalCamcoHolidayWorkedHoursOfSalaryEmployeesAsync(string employeeId)
	//	{
	//		double totalHours = 0;

	//		var holidayDates = await DatabaseContext.CamcoHolidayDates.AsNoTracking().ToListAsync();

	//		foreach (var date in holidayDates)
	//		{
	//			double totalHoursForDate = 0;

	//			var entries = await DatabaseContext.EmployeeCheckInAndOuts.AsNoTracking()
	//				.Where(entry => entry.EmployeeId == employeeId &&
	//				                entry.CheckInTime != null &&
	//				                entry.CheckOutTime != null)
	//				.ToListAsync();

	//			if (date.ToDate == null)
	//			{
	//				totalHoursForDate = entries
	//					.Where(entry => entry.CheckInTime.Value.Date == date.FromDate.Value.Date)
	//					.Select(entry => (entry.CheckOutTime.Value - entry.CheckInTime.Value).TotalHours)
	//					.Sum();
	//			}
	//			else
	//			{
	//				totalHoursForDate = entries
	//					.Where(entry => entry.CheckInTime.Value.Date >= date.FromDate.Value.Date &&
	//					                entry.CheckInTime.Value.Date <= date.ToDate.Value.Date)
	//					.Select(entry => (entry.CheckOutTime.Value - entry.CheckInTime.Value).TotalHours)
	//					.Sum();
	//			}

	//			totalHours += totalHoursForDate;
	//		}

	//		return totalHours;
	//	//}

	//	//public async Task<List<MissedOnTimeClockInOutDetailsViewModel>> GetMissedOnTimeClockInOutListAsync(
	//	//	bool isJarvisActive)
	//	//{
	//	//	var missedOnTimeClockInOutList = (
	//	//			from employee in await DatabaseContext.Employees.AsNoTracking().ToListAsync()
	//	//			join employeeSetting in await DatabaseContext.EmployeeSettings.AsNoTracking().ToListAsync() on
	//	//				employee.CustomEmployeeId equals employeeSetting.EmployeeId
	//	//			where employee.IsActive && (!isJarvisActive || employeeSetting.IsActive == true)

	//	//			join employeeCheckInAndOut in await DatabaseContext.EmployeeCheckInAndOuts.ToListAsync() on
	//	//				employee.CustomEmployeeId equals employeeCheckInAndOut.EmployeeId into
	//	//				employeeCheckInAndOutGroup
	//	//			join employeeCheckInOutRequest in await DatabaseContext.CheckInOutRequests.ToListAsync() on
	//	//				employee.CustomEmployeeId equals employeeCheckInOutRequest.EmployeeId into
	//	//				employeeCheckInOutRequestGroup
	//	//			select new MissedOnTimeClockInOutDetailsViewModel
	//	//			{
	//	//				EmployeeName = employee.FullName,

	//	//				OnTimeClockInList = (
	//	//					from checkInOut in employeeCheckInAndOutGroup
	//	//					where checkInOut.CheckInTime != null
	//	//					      && !employeeCheckInOutRequestGroup
	//	//						      .Where(req => req.RequestedCheckInTime != null)
	//	//						      .Select(req => req.CheckInOutId)
	//	//						      .Contains(checkInOut.Id)
	//	//					select new MissedOnTimeClockInOutDetailsViewModel.MissedClockTimeOrOnTime
	//	//					{
	//	//						Time = checkInOut.CheckInTime.Value.ToString("hh:mm tt"),
	//	//						Date = checkInOut.CheckInTime.Value.ToString("MM/dd/yyyy"),
	//	//						CheckDate = checkInOut.CheckInTime.Value.Date,
	//	//						Type = "DONE ON TIME"
	//	//					}
	//	//				).ToList(),

	//	//				OnTimeClockOutList = (
	//	//					from checkInOut in employeeCheckInAndOutGroup
	//	//					where checkInOut.CheckOutTime != null
	//	//					      && !employeeCheckInOutRequestGroup
	//	//						      .Where(req => req.RequestedCheckOutTime != null)
	//	//						      .Select(req => req.CheckInOutId)
	//	//						      .Contains(checkInOut.Id)
	//	//					select new MissedOnTimeClockInOutDetailsViewModel.MissedClockTimeOrOnTime
	//	//					{
	//	//						Time = checkInOut.CheckOutTime.Value.ToString("hh:mm tt"),
	//	//						Date = checkInOut.CheckOutTime.Value.ToString("MM/dd/yyyy"),
	//	//						CheckDate = checkInOut.CheckOutTime.Value.Date,
	//	//						Type = "DONE ON TIME"
	//	//					}
	//	//				).ToList(),

	//	//				MissedClockInList = (
	//	//					from checkInOutRequest in employeeCheckInOutRequestGroup
	//	//					where checkInOutRequest.OldCheckInTime == null
	//	//					      && checkInOutRequest.RequestedCheckInTime != null
	//	//					select new MissedOnTimeClockInOutDetailsViewModel.MissedClockTimeOrOnTime
	//	//					{
	//	//						Time = checkInOutRequest.RequestedCheckInTime.Value.ToString("hh:mm tt"),
	//	//						Date = checkInOutRequest.RequestedCheckInTime.Value.ToString("MM/dd/yyyy"),
	//	//						CheckDate = checkInOutRequest.RequestedCheckInTime.Value.Date,
	//	//						Type = string.IsNullOrEmpty(checkInOutRequest.ApprovedOrDisapprovedBy)
	//	//							? "PENDING REQUEST"
	//	//							: ((bool)checkInOutRequest.IsApproved
	//	//								? "APPROVED REQUEST"
	//	//								: $"DISAPPROVED REQUEST (REASON: {checkInOutRequest.DisapprovedReason})")
	//	//					}
	//	//				).ToList(),

	//	//				MissedClockOutList = (
	//	//					from checkInOut in employeeCheckInAndOutGroup
	//	//					where checkInOut.CheckOutTime == null
	//	//					select new MissedOnTimeClockInOutDetailsViewModel.MissedClockTimeOrOnTime
	//	//					{
	//	//						Date = checkInOut.CheckInTime?.ToString("MM/dd/yyyy") ?? "N/A",
	//	//						CheckDate = checkInOut.CheckInTime?.Date ?? DateTime.MinValue,
	//	//						Type = "MISSED CLOCK-OUT"
	//	//					}
	//	//				).ToList(),

	//	//				AlteredClockInList = (
	//	//					from checkInOutRequest in employeeCheckInOutRequestGroup
	//	//					where checkInOutRequest.OldCheckInTime != null
	//	//					      && checkInOutRequest.RequestedCheckInTime != null
	//	//					select new MissedOnTimeClockInOutDetailsViewModel.CheckInOutRequests
	//	//					{
	//	//						OldTime = checkInOutRequest.OldCheckInTime.Value.ToString("hh:mm tt"),
	//	//						CheckTimeDate = checkInOutRequest.RequestedCheckInTime.Value.ToString("MM/dd/yyyy"),
	//	//						RequestedTime = checkInOutRequest.RequestedCheckInTime.Value.ToString("hh:mm tt"),
	//	//						Date = checkInOutRequest.RequestedCheckInTime.Value.Date,
	//	//						RequestType = string.IsNullOrEmpty(checkInOutRequest.ApprovedOrDisapprovedBy)
	//	//							? "PENDING REQUEST"
	//	//							: ((bool)checkInOutRequest.IsApproved
	//	//								? "APPROVED REQUEST"
	//	//								: $"DISAPPROVED REQUEST (REASON: {checkInOutRequest.DisapprovedReason})")
	//	//					}
	//	//				).ToList(),

	//	//				AlteredClockOutList = (
	//	//					from checkInOutRequest in employeeCheckInOutRequestGroup
	//	//					where checkInOutRequest.OldCheckOutTime != null
	//	//					      && checkInOutRequest.RequestedCheckOutTime != null
	//	//					select new MissedOnTimeClockInOutDetailsViewModel.CheckInOutRequests
	//	//					{
	//	//						OldTime = checkInOutRequest.OldCheckOutTime.Value.ToString("hh:mm tt"),
	//	//						RequestedTime = checkInOutRequest.RequestedCheckOutTime.Value.ToString("hh:mm tt"),
	//	//						CheckTimeDate = checkInOutRequest.RequestedCheckOutTime.Value.ToString("MM/dd/yyyy"),
	//	//						Date = checkInOutRequest.RequestedCheckOutTime.Value.Date,
	//	//						RequestType = string.IsNullOrEmpty(checkInOutRequest.ApprovedOrDisapprovedBy)
	//	//							? "PENDING REQUEST"
	//	//							: ((bool)checkInOutRequest.IsApproved
	//	//								? "APPROVED REQUEST"
	//	//								: $"DISAPPROVED REQUEST (REASON: {checkInOutRequest.DisapprovedReason})")
	//	//					}
	//	//				).ToList(),
	//	//			})
	//	//		.OrderBy(x => x.EmployeeName)
	//	//		.ToList();

	//	//	return missedOnTimeClockInOutList;
	//	//}

	//}
}