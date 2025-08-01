using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.IRepositories.TaskInfo;

namespace CamcoTasks.Infrastructure.Repositories.TaskInfo;

public class TaskChangeLogRepository : Repository<TaskChangeLog>, ITaskChangeLogRepository
{
    public TaskChangeLogRepository(DatabaseContext context) : base(context)
    {
    }
}
