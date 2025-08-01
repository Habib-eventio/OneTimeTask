using CamcoTasks.Infrastructure.Entities.Logging;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.LoggingChangeLogDTO
{
    public class LoggingChangeLogDtoNew
    {
        public static ChangeLog Map(LoggingChangeLogViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new ChangeLog
            {
                Id = viewModel.Id,
                RecordId = viewModel.RecordId,
                RecordType = viewModel.RecordType,
                RecordField = viewModel.RecordField,
                OldValue = viewModel.OldValue,
                NewValue = viewModel.NewValue,
                UpdateDate = viewModel.UpdateDate,
                UpdatedBy = viewModel.UpdatedBy,
            };
        }

        public static LoggingChangeLogViewModel Map(ChangeLog entity)
        {
            if (entity == null) { return null; }

            return new LoggingChangeLogViewModel
            {
                Id = entity.Id,
                RecordId = entity.RecordId,
                RecordType = entity.RecordType,
                RecordField = entity.RecordField,
                OldValue = entity.OldValue,
                NewValue = entity.NewValue,
                UpdateDate = entity.UpdateDate,
                UpdatedBy = entity.UpdatedBy,
            };
        }

        public static IEnumerable<LoggingChangeLogViewModel> Map(IEnumerable<ChangeLog> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
