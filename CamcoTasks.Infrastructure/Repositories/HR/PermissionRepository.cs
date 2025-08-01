// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class PermissionRepository : Repository<Permission>,
	IPermissionRepository
{
	public PermissionRepository(DatabaseContext context) : base(context)
	{

	}
}