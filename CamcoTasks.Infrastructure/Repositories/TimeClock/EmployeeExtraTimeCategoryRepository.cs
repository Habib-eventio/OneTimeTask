// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.TimeClock;
using CamcoTasks.Infrastructure.IRepository.TimeClock;

namespace CamcoTasks.Infrastructure.Repository.TimeClock;

public class EmployeeExtraTimeCategoryRepository : Repository<EmployeeExtraTimeCategory>,
	IEmployeeExtraTimeCategoryRepository
{
	public EmployeeExtraTimeCategoryRepository(DatabaseContext context) : base(context)
	{

	}
}