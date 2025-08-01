// This File Needs to be reviewed Still. Don't Remove this comment.
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.IRepositories.TaskInfo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CamcoTasks.Infrastructure.IRepositories;

namespace CamcoTasks.Infrastructure.Repositories.TaskInfo
{
    public class TaskTaskTypeRepository : Repository<TaskTaskType>, ITaskTaskTypeRepository
    {
        public TaskTaskTypeRepository(DatabaseContext context) : base(context)
        {
        }

        public Task AddAsync(TaskTaskType entity) => base.AddAsync(entity);

        public Task RemoveAsync(TaskTaskType entity) => base.RemoveAsync(entity);

        public Task<TaskTaskType?> GetAsync(int id) => base.GetAsync(id);

        public Task<TaskTaskType?> FindAsync(Expression<System.Func<TaskTaskType, bool>> predicate) => base.FindAsync(predicate);

        public Task<List<TaskTaskType>> GetListAsync() => base.GetListAsync();
    }
}
