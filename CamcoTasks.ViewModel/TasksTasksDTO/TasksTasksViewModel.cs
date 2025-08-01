using CamcoTasks.Infrastructure.EnumHelper.Enums.Task;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CamcoTasks.ViewModels.TasksTasksDTO
{
    public class TasksTasksViewModel
    {
        public int Id { get; set; }
        public int? TaskId { get; set; }
        public string? TaskType { get; set; }
        public int? Priority { get; set; }
        public string? Description { get; set; }
        public string? Department { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? LatestUpdate { get; set; }
        public string? Update { get; set; }
        public string? Initiator { get; set; }
        public string? PersonResponsible { get; set; }
        public int Progress { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsReviewed { get; set; }
        public string? PictureLink { get; set; }
        public string? FileLink { get; set; }
        public int? NudgeCount { get; set; }
        public int? EmailCount { get; set; }
        public int ImagesCount { get; set; }
        public int FilesCount { get; set; }
        public bool TaskCompleted { get; set; }
        public int? ParentTaskId { get; set; }
        public bool IsSelected { get; set; }
        public DateTime? UpcomingDate { get; set; }
        public DateTime? StartDate { get; set; }
        public virtual List<TasksTaskUpdatesViewModel> TasksTaskUpdates { get; set; }
        public int? TaskStatusId { get; set; }

        public string TaskStatusDisplay
        {
            get
            {
                var status = (TaskStatusId.HasValue) ? (StatusType)TaskStatusId.Value : StatusType.Default;
                var fieldInfo = status.GetType().GetField(status.ToString());
                var attribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
                return attribute?.Name ?? status.ToString();
            }
        }
        [StringLength(2000, ErrorMessage = "Summary cannot exceed 2000 characters.")]
        public string? Summary { get; set; }
        public int? CostingCode { get; set; }
        public DateTime? LastUpdate { get; set; } = DateTime.Now.Date;
        public DateTime? DueDate { get; set; }
    }
}
