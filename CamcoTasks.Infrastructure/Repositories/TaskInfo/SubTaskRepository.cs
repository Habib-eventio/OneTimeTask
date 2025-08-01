// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.IRepositories.TaskInfo;

namespace CamcoTasks.Infrastructure.Repositories.TaskInfo;

public class SubTaskRepository : Repository<SubTask>,
	ISubTaskRepository
{
	public SubTaskRepository(DatabaseContext context) : base(context)
	{

	}
}