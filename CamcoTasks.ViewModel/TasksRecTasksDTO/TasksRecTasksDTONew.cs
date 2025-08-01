using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using System.Collections.Generic;
using System.Linq;

namespace CamcoTasks.ViewModels.TasksRecTasksDTO
{
    public class TasksRecTasksDTONew
    {
        public static RecurringTask Map(TasksRecTasksViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new RecurringTask
            {
                DateCompleted = viewModel.DateCompleted,
                Description = viewModel.Description,
                FrequencyId = viewModel.TasksFreq.Id,
                Id = viewModel.Id,
                Initiator = viewModel.Initiator,
                IsDeactivated = viewModel.IsDeactivated,
                IsDeleted = viewModel.IsDeleted,
                IsApproved = viewModel.IsApproved,
                ApprovedByEmployeeId = viewModel.ApprovedByEmployeeId,
                DateApproved = viewModel.DateApproved,
                PersonResponsible = viewModel.PersonResponsible,
                PictureLink = viewModel.PictureLink,
                DateCreated = viewModel.DateCreated,
                TaskId = viewModel.TaskId,
                UpcomingDate = viewModel.UpcomingDate,
                Updates = viewModel.Updates,
                InstructionFileLink = viewModel.InstructionFileLink,
                NudgeCount = viewModel.NudgeCount,
                EmailCount = viewModel.EmailCount,
                EmailsList = viewModel.EmailsList,
                CompletedOnTime = viewModel.CompletedOnTime,
                TasksTaskUpdates = TasksTaskUpdateDTONew.Map(viewModel.TasksTaskUpdates).ToList(),
                IsGraphRequired = viewModel.IsGraphRequired,
                AuditPerson = viewModel.AuditPerson,
                GraphTitle = viewModel.GraphTitle,
                IsTrendLine = viewModel.IsTrendLine,
                VerticalAxisTitle = viewModel.VerticalAxisTitle,
                ExternalAuditor = viewModel.ExternalAuditor,
                IsPassOrFail = viewModel.IsPassOrFail,
                Question = viewModel.Question,
                IsQuestionRequired = viewModel.IsQuestionRequired,
                IsPicRequired = viewModel.IsPicRequired,
                UpdateImageDescription = viewModel.UpdateImageDescription,
                UpdateImageType = viewModel.UpdateImageType,
                UpdateImageLocation = viewModel.UpdateImageLoction,
                FailedEmailsList = viewModel.FailedEmailsList,
                IsAuditRequired = viewModel.IsAuditRequired,
                LatestGraphValue = viewModel.LatestGraphValue,
                ParentTaskId = viewModel.ParentTaskId,
                IsDescriptionMandatory = viewModel.IsDescriptionMandatory,
                StartDate = viewModel.StartDate,
                IsMaxValueRequired = viewModel.IsMaxValueRequired,
                MaxYAxisValue = viewModel.MaxYAxisValue,
                IsHandDeliveredRequired = viewModel.IsHandDeliveredRequired,
                IsProtected = viewModel.IsProtected,
                DueDateReminder = viewModel.DueDateReminder,
                IsPicture = viewModel.IsPicture,
                TaskDescriptionSubject = viewModel.TaskDescriptionSubject,
                TaskArea = viewModel.TaskArea,
                AuthorizationList = viewModel.AuthorizationList,
                IsPositionSpecific = viewModel.IsPositionSpecific,
                JobTitle = viewModel.JobTitle,
                StartDueDateDay = viewModel.StartDueDateDay,
                EndDueDateDay = viewModel.EndDueDateDay,
                ExpectMinutes = viewModel.ExpectMinutes,
                Location = viewModel.Location,
                EmailsListJobId = viewModel.EmailsListJobId,
                IsXAxisInterval = viewModel.IsXAxisInterval,
                XAxisIntervalTypeId = viewModel.XAxisIntervalTypeId,
                XAxisIntervalRange = viewModel.XAxisIntervalRange,
                TaskStartDueDate = viewModel.TaskStartDueDate,
                TaskEndDueDate = viewModel.TaskEndDueDate,
                IsTaskDuePeriod = viewModel.IsTaskDuePeriod,
                IsTaskDelayed = viewModel.IsTaskDelayed,
                DelayReason = viewModel.DelayReason,
                TaskDelayedDate = viewModel.TaskDelayedDate,
                IsTaskRandomize = viewModel.IsTaskRandomize,
                IsDocumentRequired = viewModel.IsDocumentRequired,
                UpdatedDocumentLink = viewModel.UpdatedDocumentLink,
                UpdatedDocumentDescription = viewModel.UpdatedDocumentDescription,
                HandDocumentDeliverTo = viewModel.HandDocumentDeliverTo,
                TaskDeactivatedDate = viewModel.TaskDeactivatedDate
            };
        }

        public static TasksRecTasksViewModel Map(RecurringTask dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new TasksRecTasksViewModel
            {
                DateCompleted = dataEntity.DateCompleted,
                Description = dataEntity.Description,
                Frequency = dataEntity.TasksFrequency?.Frequency,
                Id = dataEntity.Id,
                Initiator = dataEntity.Initiator,
                IsDeactivated = dataEntity.IsDeactivated,
                IsDeleted = dataEntity.IsDeleted,
                IsApproved = dataEntity.IsApproved,
                ApprovedByEmployeeId = dataEntity.ApprovedByEmployeeId,
                DateApproved = dataEntity.DateApproved,
                PersonResponsible = dataEntity.PersonResponsible,
                PictureLink = dataEntity.PictureLink,
                DateCreated = dataEntity.DateCreated,
                TaskId = dataEntity.TaskId,
                UpcomingDate = dataEntity.UpcomingDate,
                Updates = dataEntity.Updates,
                InstructionFileLink = dataEntity.InstructionFileLink,
                NudgeCount = dataEntity.NudgeCount,
                EmailCount = dataEntity.EmailCount,
                EmailsList = dataEntity.EmailsList,
                CompletedOnTime = dataEntity.CompletedOnTime,
                IsGraphRequired = dataEntity.IsGraphRequired ?? false,
                AuditPerson = dataEntity.AuditPerson,
                GraphTitle = dataEntity.GraphTitle,
                IsTrendLine = dataEntity.IsTrendLine,
                VerticalAxisTitle = dataEntity.VerticalAxisTitle,
                ExternalAuditor = dataEntity.ExternalAuditor,
                IsPassOrFail = dataEntity.IsPassOrFail,
                Question = dataEntity.Question,
                IsQuestionRequired = dataEntity.IsQuestionRequired,
                IsPicRequired = dataEntity.IsPicRequired,
                UpdateImageDescription = dataEntity.UpdateImageDescription,
                UpdateImageType = dataEntity.UpdateImageType,
                UpdateImageLoction = dataEntity.UpdateImageLocation,
                FailedEmailsList = dataEntity.FailedEmailsList,
                IsAuditRequired = dataEntity.IsAuditRequired,
                LatestGraphValue = dataEntity.LatestGraphValue,
                ParentTaskId = dataEntity.ParentTaskId,
                IsDescriptionMandatory = dataEntity.IsDescriptionMandatory,
                StartDate = dataEntity.StartDate,
                IsMaxValueRequired = dataEntity.IsMaxValueRequired,
                MaxYAxisValue = dataEntity.MaxYAxisValue,
                IsHandDeliveredRequired = dataEntity.IsHandDeliveredRequired,
                IsProtected = dataEntity.IsProtected,
                DueDateReminder = dataEntity.DueDateReminder,
                IsPicture = dataEntity.IsPicture,
                TaskDescriptionSubject = dataEntity.TaskDescriptionSubject,
                TaskArea = dataEntity.TaskArea,
                AuthorizationList = dataEntity.AuthorizationList,
                TasksFreq = dataEntity.TasksFrequency != null ? new TasksFrequencyListViewModel() { Id = dataEntity.TasksFrequency.Id, Frequency = dataEntity.TasksFrequency.Frequency, Days = dataEntity.TasksFrequency.Days}: null,
                IsPositionSpecific = dataEntity.IsPositionSpecific,
                JobTitle = dataEntity.JobTitle,
                StartDueDateDay = dataEntity.StartDueDateDay,
                EndDueDateDay = dataEntity.EndDueDateDay,
                ExpectMinutes = dataEntity.ExpectMinutes,
                Location = dataEntity.Location,
                EmailsListJobId = dataEntity.EmailsListJobId,
                IsXAxisInterval = dataEntity.IsXAxisInterval,
                XAxisIntervalTypeId = dataEntity.XAxisIntervalTypeId,
                XAxisIntervalRange = dataEntity.XAxisIntervalRange,
                TaskStartDueDate = dataEntity.TaskStartDueDate,
                TaskEndDueDate = dataEntity.TaskEndDueDate,
                IsTaskDuePeriod = dataEntity.IsTaskDuePeriod,
                IsTaskDelayed = dataEntity.IsTaskDelayed,
                DelayReason = dataEntity.DelayReason,
                TaskDelayedDate = dataEntity.TaskDelayedDate,
                IsTaskRandomize = dataEntity.IsTaskRandomize,
                IsDocumentRequired = dataEntity.IsDocumentRequired,
                UpdatedDocumentLink = dataEntity.UpdatedDocumentLink,
                UpdatedDocumentDescription = dataEntity.UpdatedDocumentDescription,
                HandDocumentDeliverTo = dataEntity.HandDocumentDeliverTo,
                TaskDeactivatedDate = dataEntity.TaskDeactivatedDate
            };
        }

        public static IEnumerable<TasksRecTasksViewModel> Map(IEnumerable<RecurringTask> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
