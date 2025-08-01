using System.Threading.Tasks;
using ERP.Data.Entities.TimeTracking;
using CamcoTasks.Infrastructure;
using System.Linq.Expressions;

namespace ERP.Repository.IRepository.TimeTracking;

public interface IOptionRepository : IRepository<Option>
{
    Task<Option?> FindAsync(Expression<System.Func<Option, bool>> predicate);
}
