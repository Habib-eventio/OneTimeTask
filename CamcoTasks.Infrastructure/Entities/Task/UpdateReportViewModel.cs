using System;

namespace CamcoTasks.Infrastructure.Entities.Task;

public class UpdateReportViewModel
{
    public int TaskId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public int TimeSpent { get; set; }

    public string TaskDescription { get; set; }

    public double PastAverageTime { get; set; }
}