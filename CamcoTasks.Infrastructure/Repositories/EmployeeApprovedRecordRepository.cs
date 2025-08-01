// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.TimeClock;
using CamcoTasks.Infrastructure.IRepository.TimeClock;

namespace CamcoTasks.Infrastructure.Repository.TimeClock;

public class EmployeeApprovedRecordRepository : Repository<EmployeeApprovedRecord>,
	IEmployeeApprovedRecordRepository
{
	public EmployeeApprovedRecordRepository(DatabaseContext context) : base(context)
	{

	}
}