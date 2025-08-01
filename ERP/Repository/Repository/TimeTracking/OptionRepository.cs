using ERP.Data.Entities.TimeTracking;
using ERP.Repository.IRepository.TimeTracking;
using CamcoTasks.Infrastructure;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Repository.Repository.TimeTracking;

public class OptionRepository : Repository<Option>, IOptionRepository
{
    public OptionRepository(DatabaseContext context) : base(context) { }

    public Task<Option?> FindAsync(Expression<System.Func<Option, bool>> predicate) => base.FindAsync(predicate);
}
