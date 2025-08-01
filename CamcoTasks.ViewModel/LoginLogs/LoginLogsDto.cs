using CamcoTasks.Infrastructure.Entities.HR;
using ERP.Data.Entities.HR;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.LoginLogs
{
    public class LoginLogsDto
    {
        public static LoginLog Map(LoginLogsViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new LoginLog
            {
                Id = viewModel.Id,
                EmployeeId = viewModel.EmployeeId,
                SignedInTime = viewModel.SignedInTime,
                SignedOutTime = viewModel.SignedInTime,
                TotalChanges = viewModel.TotalChanges,
                ApplicationId = viewModel.ApplicationId,
            };
        }

        public static LoginLogsViewModel Map(LoginLog entity)
        {
            if (entity == null) { return null; }

            return new LoginLogsViewModel
            {
                Id = entity.Id,
                EmployeeId = entity.EmployeeId,
                SignedInTime = entity.SignedInTime,
                SignedOutTime = entity.SignedInTime,
                TotalChanges = entity.TotalChanges,
                ApplicationId = entity.ApplicationId,
            };
        }

        public static IEnumerable<LoginLogsViewModel> Map(IEnumerable<LoginLog> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
