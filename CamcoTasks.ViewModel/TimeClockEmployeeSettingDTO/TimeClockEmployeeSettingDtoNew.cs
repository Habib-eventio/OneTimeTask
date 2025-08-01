using CamcoTasks.Infrastructure.Entities.TimeClock;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.TimeClockEmployeeSettingDTO
{
    public class TimeClockEmployeeSettingDtoNew
    {
        public static EmployeeSetting Map(TimeClockEmployeeSettingViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new EmployeeSetting
            {
                Id = viewModel.Id,
                EmployeeId = viewModel.EmpId,
                IsManager = viewModel.IsManager,
                IsActive = viewModel.IsActive,
                IsRemotelyWorking = viewModel.IsRemotelyWorking,
                EmailSentDate = viewModel.EmailSentDate,
                ExpectedClockInTime = viewModel.ExpectedClockInTime,
                ExpectedClockOutTime = viewModel.ExpectedClockOutTime,
            };
        }

        public static TimeClockEmployeeSettingViewModel Map(EmployeeSetting dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new TimeClockEmployeeSettingViewModel
            {
                Id = dataEntity.Id,
                EmpId = dataEntity.EmployeeId,
                IsManager = dataEntity.IsManager,
                IsActive = dataEntity.IsActive,
                IsRemotelyWorking = dataEntity.IsRemotelyWorking,
                EmailSentDate = dataEntity.EmailSentDate,
                ExpectedClockInTime = dataEntity.ExpectedClockInTime,
                ExpectedClockOutTime = dataEntity.ExpectedClockOutTime,
            };
        }

        public static IEnumerable<TimeClockEmployeeSettingViewModel> Map(IEnumerable<EmployeeSetting> dataList)
        {
            if (dataList == null) { yield break; }
            foreach (var item in dataList)
            {
                yield return Map(item);
            }
        }

        public static IEnumerable<EmployeeSetting> Map(IEnumerable<TimeClockEmployeeSettingViewModel> dataList)
        {
            if (dataList == null) { yield break; }
            foreach (var item in dataList)
            {
                yield return Map(item);
            }
        }
    }
}
