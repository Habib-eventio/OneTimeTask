// This File Needs to be reviewed Still. Don't Remove this comment.
using CamcoTasks.Infrastructure.Entities.Maintenance;
using CamcoTasks.Infrastructure.IRepositories.Maintenance;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure.Repositories.Maintenance
{
    public class WorkOrderDataRepository : Repository<WorkOrderData>, IWorkOrderDataRepository
    {
        public WorkOrderDataRepository(DatabaseContext context) : base(context)
        {
        }

        public Task<List<WorkOrderData>> GetListAsync() => base.GetListAsync();

        public Task<List<WorkOrderData>> FindAllAsync(Expression<System.Func<WorkOrderData, bool>> predicate) => base.FindAllAsync(predicate);
    }
}
