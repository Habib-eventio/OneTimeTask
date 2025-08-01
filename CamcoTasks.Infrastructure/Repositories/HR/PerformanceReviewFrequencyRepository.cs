//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class PerformanceReviewFrequencyRepository : Repository<PerformanceReviewFrequency>,
//	IPerformanceReviewFrequencyRepository
//{
//	public PerformanceReviewFrequencyRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<string> GetPerformanceReviewFrequencyIdByFrequencyIdAsync(long enumFrequencyId)
//	{
//		return await (from performanceReviewFrequency in DatabaseContext.PerformanceReviewFrequencies
//			where performanceReviewFrequency.Id == enumFrequencyId
//			select performanceReviewFrequency.FrequencyName).FirstOrDefaultAsync();
//	}

//	public async Task<long> GetIdFromPerformanceReviewFrequencyByMonthsAsync(double months)
//	{
//		var frequencies = await (from performanceReviewFrequency in DatabaseContext.PerformanceReviewFrequencies
//			select performanceReviewFrequency).ToListAsync();

//		var frequency = frequencies.FirstOrDefault(a => a.FrequencyName == "Daily");
//		if (frequency != null)
//		{
//			if (months >= frequency.FrequencyDurationMonthStart && months < frequency.FrequencyDurationMonthEnd)
//			{
//				return frequency.Id;
//			}
//		}

//		frequency = frequencies.FirstOrDefault(a => a.FrequencyName == "Weekly");
//		if (frequency != null)
//		{
//			if (months >= frequency.FrequencyDurationMonthStart && months < frequency.FrequencyDurationMonthEnd)
//			{
//				return frequency.Id;
//			}
//		}

//		frequency = frequencies.FirstOrDefault(a => a.FrequencyName == "Bi-Weekly");
//		if (frequency != null)
//		{
//			if (months >= frequency.FrequencyDurationMonthStart && months < frequency.FrequencyDurationMonthEnd)
//			{
//				return frequency.Id;
//			}
//		}

//		frequency = frequencies.FirstOrDefault(a => a.FrequencyName == "Bi-Monthly");
//		if (frequency != null)
//		{
//			if (months >= frequency.FrequencyDurationMonthStart && months < frequency.FrequencyDurationMonthEnd)
//			{
//				return frequency.Id;
//			}
//		}

//		frequency = frequencies.FirstOrDefault(a => a.FrequencyName == "Monthly");
//		if (frequency != null)
//		{
//			if (months >= frequency.FrequencyDurationMonthStart && months < frequency.FrequencyDurationMonthEnd)
//			{
//				return frequency.Id;
//			}
//		}

//		frequency = frequencies.FirstOrDefault(a => a.FrequencyName == "Six Months");
//		if (frequency != null)
//		{
//			if (months >= frequency.FrequencyDurationMonthStart && months < frequency.FrequencyDurationMonthEnd)
//			{
//				return frequency.Id;
//			}
//		}

//		frequency = frequencies.FirstOrDefault(a => a.FrequencyName == "Eight Months");
//		if (frequency != null)
//		{
//			if (months >= frequency.FrequencyDurationMonthStart && months < frequency.FrequencyDurationMonthEnd)
//			{
//				return frequency.Id;
//			}
//		}

//		frequency = frequencies.FirstOrDefault(a => a.FrequencyName == "Yearly");
//		if (frequency != null)
//		{
//			if (months >= frequency.FrequencyDurationMonthStart)
//			{
//				return frequency.Id;
//			}
//		}

//		return 0;
//	}
//}