using CamcoTasks.Infrastructure.Entities.Maintenance;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure.IRepositories.Maintenance
{
    public interface IWorkOrderDataRepository
    {
        Task<List<WorkOrderData>> GetListAsync();
        Task<List<WorkOrderData>> FindAllAsync(Expression<System.Func<WorkOrderData, bool>> predicate);
    }
}
