using CamcoTasks.Infrastructure.Entities.HR;
using System;

namespace CamcoTasks.Infrastructure.Entities;
public class ProjectCosting
{
    public int ProjectId { get; set; }

    public string SoNumber { get; set; }

    public string Notes { get; set; }

    public string Status { get; set; }

    public Employee Employee { get; set; }

    public string Champion { get; set; }

    public DateTime? LastActivityDate { get; set; }

    public string ProjectTitle { get; set; }

    public double HourlyRate { get; set; }

    public DateTime? DateCreated { get; set; }

    public double? HoursSpent { get; set; }

    public double LastWeekHoursSpent { get; set; }

    public double TotalCost { get; set; }

    public double LastWeekCostSpent { get; set; }

    public DateTime Date { get; set; }

    public string Hours { get; set; }

    public string Description { get; set; }
}