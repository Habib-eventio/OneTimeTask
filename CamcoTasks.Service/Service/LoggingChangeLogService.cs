using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.LoggingChangeLogDTO;
using CamcoTasks.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service
{
    public class LoggingChangeLogService : ILoggingChangeLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoggingChangeLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> InsertAsync(LoggingChangeLogViewModel entity)
        {
            var result = LoggingChangeLogDtoNew.Map(entity);
            await _unitOfWork.ChangeLogs.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            return result.Id;
        }

        public async Task<IEnumerable<LoggingChangeLogViewModel>> GetListAsync(string recordType,
            int recordId)
        {
            var entities = await _unitOfWork.ChangeLogs.FindAllAsync(l =>
                l.RecordType == recordType && l.RecordId == recordId);
            return LoggingChangeLogDtoNew.Map(entities);
        }
    }
}
