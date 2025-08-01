using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TimeClockEmployeeSettingDTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using CamcoTasks.Infrastructure;


namespace CamcoTasks.Service.Service
{
    public class TimeClockEmployeeSettingService : ITimeClockEmployeeSettingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimeClockEmployeeSettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TimeClockEmployeeSettingViewModel>> GetListAsync(bool IsActive)
        {
            var result = await _unitOfWork.EmployeeSettings.FindAllWithOrderByAscendingAsync(x => x.IsActive.HasValue && x.IsActive == IsActive,
                x => x.Id);
            return TimeClockEmployeeSettingDtoNew.Map(result);
        }

        public async Task<IEnumerable<string>> GetEmpIdListAsync(bool isAcitve)
        {
            return await _unitOfWork.EmployeeSettings.GetAllEmployeeIdsByActiveStateAsync(isAcitve);
        }
    }
}
