// This File Needs to be reviewed Still. Don't Remove this comment.
using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class DisciplineLogRepository : Repository<DisciplineLog>,
	IDisciplineLogRepository
{
	public DisciplineLogRepository(DatabaseContext context) : base(context)
	{

	}
}