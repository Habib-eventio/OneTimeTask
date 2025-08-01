// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.Automated;
using CamcoTasks.Infrastructure.IRepository.Automated;

namespace CamcoTasks.Infrastructure.Repository.Automated;

public class AutomationRepository : Repository<Automation>,
	IAutomationRepository
{
	public AutomationRepository(DatabaseContext context) : base(context)
	{

	}
}