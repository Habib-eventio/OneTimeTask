using CamcoTasks.ViewModels.LoginLogs;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ILoginLogsService
    {
        Task UpdateAsync(LoginLogsViewModel entity);
        Task<LoginLogsViewModel> GetAsync(short appId, long employeeId);
    }
}
