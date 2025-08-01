// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class EmployeeViewLogRepository : Repository<EmployeeViewLog>,
	IEmployeeViewLogRepository
{
	public EmployeeViewLogRepository(DatabaseContext context) : base(context)
	{

	}
}