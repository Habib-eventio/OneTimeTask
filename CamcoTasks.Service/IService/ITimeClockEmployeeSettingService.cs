using CamcoTasks.ViewModels.TimeClockEmployeeSettingDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ITimeClockEmployeeSettingService
    {
        Task<IEnumerable<TimeClockEmployeeSettingViewModel>> GetListAsync(bool isAcitve);
        Task<IEnumerable<string>> GetEmpIdListAsync(bool isAcitve);
    }
}
