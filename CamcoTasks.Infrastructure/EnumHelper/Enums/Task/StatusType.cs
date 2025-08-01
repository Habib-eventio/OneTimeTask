using System.ComponentModel.DataAnnotations;

namespace CamcoTasks.Infrastructure.EnumHelper.Enums.Task;

public enum StatusType
{
    [Display(Name = "In Progress")] InProgress,
    [Display(Name = "Pending")] Pending,
    [Display(Name = "Waiting for Review")] WaitingForReview,
    [Display(Name = "Tabled")] Tabled,
    [Display(Name = "Temporary Tabled")] TemporaryTabled,
    [Display(Name = "Done")] Done,
    [Display(Name = "Default")] Default
}