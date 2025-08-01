using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.IRepositories.TaskInfo;

namespace CamcoTasks.Infrastructure.Repository.TaskInfo;

public class TaskEditorRepository : Repository<TaskEditor>, ITaskEditorRepository
{
	public TaskEditorRepository(DatabaseContext context) : base(context)
	{

	}
}