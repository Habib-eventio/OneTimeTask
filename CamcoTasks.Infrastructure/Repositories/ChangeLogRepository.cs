// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.Logging;
using CamcoTasks.Infrastructure.IRepository.Logging;

namespace CamcoTasks.Infrastructure.Repository.Logging;

public class ChangeLogRepository : Repository<ChangeLog>,
	IChangeLogRepository
{
	public ChangeLogRepository(DatabaseContext context) : base(context)
	{

	}
}