using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using System;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.TasksRecTasksDTO
{
    public class TasksRecTasksViewModel
    {
        public int Id { get; set; }
        public int? TaskId { get; set; }
        public string TaskDescriptionSubject { get; set; }
        public string PersonResponsible { get; set; }
        public string AuditPerson { get; set; }
        public string? Frequency { get; set; }
        public string Description { get; set; }
        public string TaskArea { get; set; }
        public string Question { get; set; }
        public string? QuestionAnswer { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? UpcomingDate { get; set; }
        public DateTime? TaskStartDueDate { get; set; }
        public DateTime? TaskEndDueDate { get; set; }
        public DateTime? TaskDelayedDate { get; set; }
        public bool IsTaskDuePeriod { get; set; } = false;
        public string Updates { get; set; }
        public string Initiator { get; set; }
        public string EditBy { get; set; }
        public string PictureLink { get; set; }
		public string InstructionFileLink { get; set; }
		public string CompletedOnTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApproved { get; set; }
        public long? ApprovedByEmployeeId { get; set; }
        public DateTime? DateApproved { get; set; }
        public bool IsDeactivated { get; set; }
        public int NudgeCount { get; set; }
        public int? EmailCount { get; set; }
        public string EmailsList { get; set; }
        public bool IsGraphRequired { get; set; } = false;
        public bool? IsPassOrFail { get; set; } = false;
        public bool? IsAuditRequired { get; set; } = false;
        public bool? IsDescriptionMandatory { get; set; } = false;
        public bool? IsPicRequired { get; set; } = false;
        public bool? IsQuestionRequired { get; set; } = false;
        public bool? IsPasswordRequired { get; set; } = false;
        public bool? IsHandDeliveredRequired { get; set; } = false;
        public int? UpdateImageType { get; set; } = 1;
        public string UpdateImageLoction { get; set; } = "";
        public string UpdateImageDescription { get; set; } = "";
        public string FailedEmailsList { get; set; }
        public string GraphTitle { get; set; }
        public string VerticalAxisTitle { get; set; }
        public bool? IsTrendLine { get; set; } = false;
        public string TrendLineTitle { get; set; }
        public bool? IsMaxValueRequired { get; set; } = false;
        public bool? DueDateReminder { get; set; } = false;
        public int? MaxYAxisValue { get; set; }
        public string ExternalAuditor { get; set; }
        public int ImagesCount { get; set; }
        public int FilesCount { get; set; }
        public double NumberOfMisses { get; set; }
        public double TotalNumber { get; set; }
        public double TaskPercentage { get; set; }
        public decimal? LatestGraphValue { get; set; }
        public int? ParentTaskId { get; set; }
        public bool? IsProtected { get; set; } = false;
        public string AuthorizationList { get; set; }
        public bool? IsPicture { get; set; } = false;
        public List<EmployeeEmail> SelectedEmails { get; set; }
        public bool IsPositionSpecific { get; set; } = false;
        public bool IsTaskRandomize { get; set; } = false;
        public string? JobTitle { get; set; }
        public string StartDueDateDay { get; set; }
        public string EndDueDateDay { get; set; }
        public int? ExpectMinutes { get; set; }
        public string Location { get; set; }
        public string EmailsListJobId { get; set; }
        public bool? IsXAxisInterval { get; set; }
        public int? XAxisIntervalTypeId { get; set; }
        public double? XAxisIntervalRange { get; set; }
        public double TaskDelayedDays { get; set; }
        public string DelayReason { get; set; }
        public bool IsTaskDelayed { get; set; } = false;
        public bool IsDocumentRequired { get; set; } = false;
        public string UpdatedDocumentLink { get; set; }
        public string UpdatedDocumentDescription { get; set; }
        public string HandDocumentDeliverTo { get; set; }
        public DateTime? TaskDeactivatedDate { get; set; }

        public virtual TasksFrequencyListViewModel TasksFreq { get; set; }
        public virtual List<TasksTaskUpdatesViewModel> TasksTaskUpdates { get; set; }

        public TasksRecTasksViewModel()
        {
            TasksFreq = new TasksFrequencyListViewModel();
            TasksTaskUpdates = new List<TasksTaskUpdatesViewModel>();
        }
    }

    public class CustomGraphModel
    {
        public int GraphNumber { get; set; }
        public string GraphDate { get; set; }
    }

    public class EmployeeButton
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Percentage { get; set; }
    }
}
