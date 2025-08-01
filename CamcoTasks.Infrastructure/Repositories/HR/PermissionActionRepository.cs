// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class PermissionActionRepository : Repository<PermissionAction>,
	IPermissionActionRepository
{
	public PermissionActionRepository(DatabaseContext context) : base(context)
	{

	}
}