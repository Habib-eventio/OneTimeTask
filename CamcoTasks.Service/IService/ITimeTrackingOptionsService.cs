using CamcoTasks.ViewModels.TimeTrackingOptionsDTO;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ITimeTrackingOptionsService
    {
        Task<TimeTrackingOptionsViewModel> GetAsync(string appName, string optionName);
    }
}
