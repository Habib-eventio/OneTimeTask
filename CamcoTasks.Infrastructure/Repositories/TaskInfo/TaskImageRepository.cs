// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.IRepositories;


namespace CamcoTasks.Infrastructure.Repository.TaskInfo;

public class TaskImageRepository : Repository<TaskImage>,
	ITaskImageRepository
{
	public TaskImageRepository(DatabaseContext context) : base(context)
	{

	}
}