using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.IRepositories.TaskInfo;

namespace CamcoTasks.Infrastructure.Repository.TaskInfo;

public class TaskFolderRepository : Repository<TaskFolder>,
	ITaskFolderRepository
{
	public TaskFolderRepository(DatabaseContext dbContext) : base(dbContext)
	{

	}
}