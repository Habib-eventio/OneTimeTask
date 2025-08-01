using ERP.Data.Entities.Production;
using ERP.Repository.IRepository.Production;
using CamcoTasks.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repository.Repository.Production;

public class TimeSheetDatumRepository : Repository<TimeSheetDatum>, ITimeSheetDatumRepository
{
    public TimeSheetDatumRepository(DatabaseContext context) : base(context) { }

    public Task<double> GetSumOfBurdenTimeAsync(int projectId)
    {
        // simple sum from context
        var total = Context.Set<TimeSheetDatum>()
            .Where(t => t.ProjectId == projectId && t.BurdenTime.HasValue)
            .Sum(t => t.BurdenTime.Value);
        return Task.FromResult(total);
    }
}
