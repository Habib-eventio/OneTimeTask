// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using ERP.Data.Entities.HR;
using ERP.Repository.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class PerformanceImprovementPlanFollowupRepository : Repository<PerformanceImprovementPlanFollowup>,
	IPerformanceImprovementPlanFollowupRepository
{
	public PerformanceImprovementPlanFollowupRepository(DatabaseContext context) : base(context)
	{

	}
}