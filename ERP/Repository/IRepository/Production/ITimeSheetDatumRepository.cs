using System.Threading.Tasks;
using ERP.Data.Entities.Production;
using CamcoTasks.Infrastructure;

namespace ERP.Repository.IRepository.Production;

public interface ITimeSheetDatumRepository : IRepository<TimeSheetDatum>
{
    Task<double> GetSumOfBurdenTimeAsync(int projectId);
}
