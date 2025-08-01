// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class PerformanceImprovementPlanRepository : Repository<PerformanceImprovementPlan>,
	IPerformanceImprovementPlanRepository
{
	public PerformanceImprovementPlanRepository(DatabaseContext context) : base(context)
	{

	}
}