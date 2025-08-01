using CamcoTasks.ViewModels.TimeTrackingMiscellaneousDTO;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ITimeTrackingMiscellaneousesService
    {
        Task<int> InsertAsync(TimeTrackingMiscellaneousViewModel entity);
        Task<TimeTrackingMiscellaneousViewModel> GetLastEntityAsync();
    }
}
