using CamcoTasks.ViewModels.LoggingChangeLogDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ILoggingChangeLogService
    {
        Task<int> InsertAsync(LoggingChangeLogViewModel entity);
        Task<IEnumerable<LoggingChangeLogViewModel>> GetListAsync(string recordType,
            int recordId);
    }
}
