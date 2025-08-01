using CamcoTasks.Infrastructure.Entities.TaskInfo;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.TasksTaskUpdatesDTO
{
    public class TasksTaskUpdateDTONew
    {
        public static TaskUpdate Map(TasksTaskUpdatesViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new TaskUpdate
            {
                TaskId = viewModel.TaskID,
                IsDeleted = viewModel.IsDeleted,
                PictureLink = viewModel.PictureLink,
                RecurringId = viewModel.RecurringID,
                Update = viewModel.Update,
                UpdateDate = viewModel.UpdateDate,
                UpdateId = viewModel.UpdateId,
                FileLink = viewModel.FileLink,
                IsPicture = viewModel.IsPicture,
                IsAudit = viewModel.IsAudit,
                IsPass = viewModel.IsPass,
                GraphNumber = viewModel.GraphNumber,
                DueDate = viewModel.DueDate,
                TaskCompleted = viewModel.TaskCompleted,
                QuestionAnswer = viewModel.QuestionAnswer,
                FailReason = viewModel.FailReason,
                FailedAuditList = viewModel.FailedAuditList,
                EmailId = viewModel.EmailId,
                CreatedDate = viewModel.CreatedDate,
                PostponeReason = viewModel.PostponeReason,
                PostponeDays = viewModel.PostponeDays,
                UpdateBy = viewModel.UpdateBy,
                UpdatedDocumentLink = viewModel.UpdatedDocumentLink,

            };
        }

        public static TasksTaskUpdatesViewModel Map(TaskUpdate dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new TasksTaskUpdatesViewModel
            {
                TaskID = dataEntity.TaskId,
                IsDeleted = dataEntity.IsDeleted,
                PictureLink = dataEntity.PictureLink,
                RecurringID = dataEntity.RecurringId,
                Update = dataEntity.Update,
                UpdateDate = dataEntity.UpdateDate,
                UpdateId = dataEntity.UpdateId,
                FileLink = dataEntity.FileLink,
                IsPicture = dataEntity.IsPicture,
                IsAudit = dataEntity.IsAudit,
                QuestionAnswer = dataEntity.QuestionAnswer,
                IsPass = dataEntity.IsPass,
                GraphNumber = dataEntity.GraphNumber,
                DueDate = dataEntity.DueDate,
                TaskCompleted = dataEntity.TaskCompleted,
                FailReason = dataEntity.FailReason,
                FailedAuditList = dataEntity.FailedAuditList,
                EmailId = dataEntity.EmailId,
                CreatedDate = dataEntity.CreatedDate,
                PostponeReason = dataEntity.PostponeReason,
                PostponeDays = dataEntity.PostponeDays,
                UpdateBy = dataEntity.UpdateBy,
                UpdatedDocumentLink = dataEntity.UpdatedDocumentLink,
            };
        }

        public static IEnumerable<TasksTaskUpdatesViewModel> Map(IEnumerable<TaskUpdate> dataList)
        {
            if (dataList == null) { yield break; }
            foreach (var item in dataList)
            {
                yield return Map(item);
            }
        }

        public static IEnumerable<TaskUpdate> Map(IEnumerable<TasksTaskUpdatesViewModel> dataList)
        {
            if (dataList == null) { yield break; }
            foreach (var item in dataList)
            {
                yield return Map(item);
            }
        }
    }
}
